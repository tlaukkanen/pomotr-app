import React, {useState, useEffect} from 'react';
import { tableIcons } from './TableConfig';
import MaterialTable from 'material-table';

export default function FamilyTable() {

  const [data, setData] = useState([]);

  useEffect(() => {
    fetch("/api/families")
      .then(response => response.json())
      .then(data => setData(data));
  }, [])

  return(
    <MaterialTable 
        icons={tableIcons} 
        columns={[
          { title: "Name", field: "name" }
        ]}
        title="Family Members"
        data={data}
        editable={{
          onRowAdd: newData =>
          new Promise((resolve, reject) => {
            setTimeout(() => {
              {
                //const data = data;
                data.push(newData);
                this.setState({ data }, () => resolve());
              }
              resolve()
            }, 1000)
          }),
        onRowUpdate: (newData, oldData) =>
          new Promise((resolve, reject) => {
            setTimeout(() => {
              {
                //const data = data;
                const index = data.indexOf(oldData);
                data[index] = newData;
                this.setState({ data }, () => resolve());
              }
              resolve()
            }, 1000)
          }),
        onRowDelete: oldData =>
          new Promise((resolve, reject) => {
            setTimeout(() => {
              {
                //let data = data;
                const index = data.indexOf(oldData);
                data.splice(index, 1);
                this.setState({ data }, () => resolve());
              }
              resolve()
            }, 1000)
          }),
        }}
    />
  );
}