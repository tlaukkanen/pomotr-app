using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Authentication.Identity.Service.Extensions
{

    /// <summary>
    /// Extension methods for using Azure Key Vault with <see cref="IIdentityServerBuilder"/>.
    /// </summary>
    public static class IdentityServerAzureKeyVaultConfigurationExtensions
    {
        /// <summary>
        /// Adds a SigningCredentialStore and a ValidationKeysStore that reads the signing certificate from the Azure KeyVault.
        /// </summary>
        /// <param name="identityServerbuilder">The <see cref="IIdentityServerBuilder"/> to add to.</param>
        /// <param name="vault">The Azure KeyVault uri.</param>
        /// <param name="certificateName">The name of the certificate to use as the signing certificate.</param>
        /// <param name="clientId">The application client id.</param>
        /// <param name="clientSecret">The client secret to use for authentication.</param>
        /// <returns>The <see cref="IIdentityServerBuilder"/>.</returns>
        public static IIdentityServerBuilder AddSigningCredentialFromAzureKeyVault(this IIdentityServerBuilder identityServerbuilder, string vault, string certificateName, string clientId, string clientSecret, Action<AzureKeyVaultSigningCredentialOptions> configureOptions = null)
        {
            KeyVaultClient.AuthenticationCallback authenticationCallback = (authority, resource, scope) => GetTokenFromClientSecret(authority, resource, clientId, clientSecret);
            return AddSigningCredentialFromAzureKeyVaultInternal(identityServerbuilder, authenticationCallback, vault, certificateName, configureOptions);
        }

        /// <summary>
        /// Adds a SigningCredentialStore and a ValidationKeysStore that reads the signing certificate from the Azure KeyVault.
        /// </summary>
        /// <param name="identityServerbuilder">The <see cref="IIdentityServerBuilder"/> to add to.</param>
        /// <param name="vault">The Azure KeyVault uri.</param>
        /// <param name="certificateName">The name of the certificate to use as the signing certificate.</param>
        /// <remarks>Use this if you are using MSI (Managed Service Identity)</remarks>
        /// <returns>The <see cref="IIdentityServerBuilder"/>.</returns>
        public static IIdentityServerBuilder AddSigningCredentialFromAzureKeyVault(this IIdentityServerBuilder identityServerbuilder, string vault, string certificateName, Action<AzureKeyVaultSigningCredentialOptions> configureOptions = null)
        {
            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            var authenticationCallback = new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback);
            return AddSigningCredentialFromAzureKeyVaultInternal(identityServerbuilder, authenticationCallback, vault, certificateName, configureOptions);
        }

        private static IIdentityServerBuilder AddSigningCredentialFromAzureKeyVaultInternal(this IIdentityServerBuilder identityServerbuilder, KeyVaultClient.AuthenticationCallback authenticationCallback, string vault, string certificateName, Action<AzureKeyVaultSigningCredentialOptions> configureOptions)
        {
            identityServerbuilder.Services
                .AddMemoryCache()
                .Configure<AzureKeyVaultSigningCredentialOptions>(opts =>
                {
                    opts.KeyVaultUrl = vault;
                    opts.CertificateName = certificateName;
                    configureOptions?.Invoke(opts);
                })
                .AddSingleton<IKeyVaultClient>(new KeyVaultClient(authenticationCallback))
                .AddTransient<ISigningCredentialStore, AzureKeyVaultKeyProvider>()
                .AddTransient<IValidationKeysStore, AzureKeyVaultKeyProvider>();

            return identityServerbuilder;
        }

        private static async Task<string> GetTokenFromClientSecret(string authority, string resource, string clientId, string clientSecret)
        {
            var authContext = new AuthenticationContext(authority);
            var clientCred = new ClientCredential(clientId, clientSecret);
            var result = await authContext.AcquireTokenAsync(resource, clientCred);
            return result.AccessToken;
        }
    }

    /// <summary>
    /// Options for the AzureKeyVaultSigningCredentials
    /// </summary>
    public class AzureKeyVaultSigningCredentialOptions
    {
        /// <summary>
        /// The Azure KeyVault uri.
        /// </summary>
        public string KeyVaultUrl { get; set; }

        /// <summary>
        /// The name of the certificate to use as the signing certificate.
        /// </summary>
        public string CertificateName { get; set; }

        /// <summary>
        /// Delay in hours before a new version of the certificate can be used as the signing credential.
        /// </summary>
        public int RolloverDelayHours { get; set; } = 48;

        /// <summary>
        /// Time between refreshes of the keys from the Azure Key Vault.
        /// </summary>
        public int RefreshIntervalHours { get; set; } = 24;
    }

    /// <summary>
    /// Implementation of the <see cref="ISigningCredentialStore"/> and <see cref="IValidationKeysStore"/>
    /// that leverages a certificate loaded from an AzureKeyVault.
    /// </summary>
    internal class AzureKeyVaultKeyProvider : ISigningCredentialStore, IValidationKeysStore
    {
        private const string CacheKey = "IdentityServerSigningKeys";
        private class SigningKeys
        {
            public SigningCredentials SigningCredentials { get; set; }
            public IEnumerable<SecurityKeyInfo> ValidationKeys { get; set; }
        }

        private readonly IMemoryCache _cache;
        private readonly IKeyVaultClient _keyVaultClient;
        private readonly AzureKeyVaultSigningCredentialOptions _options;

        public AzureKeyVaultKeyProvider(IMemoryCache memoryCache, IKeyVaultClient keyVaultClient, IOptions<AzureKeyVaultSigningCredentialOptions> options)
        {
            _cache = memoryCache;
            _keyVaultClient = keyVaultClient;
            _options = options.Value;
        }

        public async Task<SigningCredentials> GetSigningCredentialsAsync()
        {
            var keys = await GetKeysInternal();
            return keys.SigningCredentials;
        }

        public async Task<IEnumerable<SecurityKeyInfo>> GetValidationKeysAsync()
        {
            var keys = await GetKeysInternal();
            return keys.ValidationKeys;
        }

        private async Task<SigningKeys> GetKeysInternal()
        {
            if (!_cache.TryGetValue(CacheKey, out SigningKeys keys))
            {
                var certificates = await GetAllCertificateVersions(_options.KeyVaultUrl, _options.CertificateName);
                var signingCert = GetLatestCertificateWithRolloverDelay(certificates, _options.RolloverDelayHours);
                if (signingCert == null)
                {
                    throw new Exception("No valid certificate available for use as signing credentials!");
                }

                keys = new SigningKeys
                {
                    SigningCredentials = new SigningCredentials(new X509SecurityKey(signingCert), SecurityAlgorithms.RsaSha256),
                    ValidationKeys =
                      var keyInfo = new SecurityKeyInfo { Key = key, SigningAlgorithm = SecurityAlgorithms.RsaSha256 };
                        certificates.Select(c => new X509SecurityKey(c)).ToList()
                };

                _cache.Set(CacheKey, keys, DateTimeOffset.UtcNow.AddHours(_options.RefreshIntervalHours));
            }

            return keys;
        }

        private async Task<List<X509Certificate2>> GetAllCertificateVersions(string keyVaultUrl, string certificateName)
        {
            var certificates = new List<X509Certificate2>();

            // Get the first page of certificates
            var certificateItemsPage = await _keyVaultClient.GetCertificateVersionsAsync(keyVaultUrl, certificateName);
            while (true)
            {
                foreach (var certificateItem in certificateItemsPage)
                {
                    // Ignored disabled or expired certificates
                    if (certificateItem.Attributes.Enabled == true &&
                        (certificateItem.Attributes.Expires == null || certificateItem.Attributes.Expires > DateTime.UtcNow))
                    {
                        var certificateVersionBundle = await _keyVaultClient.GetCertificateAsync(certificateItem.Identifier.Identifier);
                        var certificatePrivateKeySecretBundle = await _keyVaultClient.GetSecretAsync(certificateVersionBundle.SecretIdentifier.Identifier);
                        var privateKeyBytes = Convert.FromBase64String(certificatePrivateKeySecretBundle.Value);
                        var certificateWithPrivateKey = new X509Certificate2(privateKeyBytes, (string)null, X509KeyStorageFlags.MachineKeySet);

                        certificates.Add(certificateWithPrivateKey);
                    }
                }

                if (certificateItemsPage.NextPageLink == null)
                {
                    break;
                }
                else
                {
                    // Get the next page
                    certificateItemsPage = await _keyVaultClient.GetCertificateVersionsNextAsync(certificateItemsPage.NextPageLink);
                }
            }

            return certificates;
        }

        private X509Certificate2 GetLatestCertificateWithRolloverDelay(List<X509Certificate2> certificates, int rolloverDelayHours)
        {
            // First limit the search to just those certificates that have existed longer than the rollover delay.
            var rolloverCutoff = DateTime.Now.AddHours(-rolloverDelayHours);
            var potentialCerts = certificates.Where(c => c.NotBefore < rolloverCutoff);

            // If no certs could be found, then widen the search to any usable certificate.
            if (!potentialCerts.Any())
            {
                potentialCerts = certificates.Where(c => c.NotBefore < DateTime.Now);
            }

            // Of the potential certs, return the newest one.
            return potentialCerts
                .OrderByDescending(c => c.NotBefore)
                .FirstOrDefault();
        }
    }

}