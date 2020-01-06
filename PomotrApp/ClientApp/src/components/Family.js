import React from 'react';
import { tableIcons } from './TableConfig';
import MaterialTable from 'material-table';

export default function FamilyTable() {



  return(
    <MaterialTable 
        icons={tableIcons} 
        columns={[
          { title: "Name", field: "name" }
        ]}/>
  );
}