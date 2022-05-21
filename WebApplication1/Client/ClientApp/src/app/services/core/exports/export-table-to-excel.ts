import {Injectable} from '@angular/core';
import * as XLSX from 'xlsx';
import {saveFile} from '../../../utils/file/file-bridge';

const EXCEL_TYPE = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8';
const EXCEL_EXTENSION = '.xlsx';

@Injectable()
export class ExportTableToExcelService {

  constructor() { }

  public static tableToJSON(table, fileName) {
    const exporData = [], headers = [], fields = [], types = [];

    // Находим отображаемые поля, заголовки, типы
    for (let i = 0; i < table.visibleColumns.length; i++) {
      headers[i] = table.visibleColumns[i].header;
      fields[i] = table.visibleColumns[i].field;
      types[i] = table.visibleColumns[i].type;
    }

    // Обрабатываем каждую строку
    for (let i = 0; i < table.value.length; i++) {
      const rowData = {};
      const tableRow = table.value[i];

      // Работаем с каждой колонкой tableRow и реализуем ассоциативный массив
      for (let j = 0; j < fields.length; j++)  {
        let cell;
        let parseField = fields[j].split('.');
        let g = 0;
        // Лезем на дно древовидной структуры
        do {
          cell = cell ? cell[parseField[g]] : tableRow[parseField[g]];
          g++;
          switch (types[j]) {
            case 'bool': {
              cell = cell ? 'Да' : 'Нет';
              break;
            }
            case 'date': {
              cell = cell ? new Date(cell).toLocaleString() : null;
              break;
            }
            case 'dateTime': {
              cell = cell ? new Date(cell).toLocaleString() : null;
              break;
            }
            case 'enum': {
              let enumValues = table.visibleColumns[j].enum;
              if (!parseField[g]) {
                cell = cell ? enumValues[cell] : null;
              }
            }
          }
        }
        while (parseField[g]) ;
        rowData[ headers[j] ] = cell;
      }
      exporData.push(rowData);
    }

    ExportTableToExcelService.exportAsExcelFile(exporData, fileName);
  }

  public static exportAsExcelFile(json: any[], excelFileName: string): void {
    const worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(json,  {skipHeader: false});
    const workbook: XLSX.WorkBook = {
      Sheets: { 'Лист 1' : worksheet },
      SheetNames: ['Лист 1']
    };
    const excelBuffer: any = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
    saveFile(excelBuffer, excelFileName + EXCEL_EXTENSION, EXCEL_TYPE);
  }
}
