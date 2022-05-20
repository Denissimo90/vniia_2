import * as FileSaver from 'file-saver';

const saveAs =  FileSaver.saveAs;

export const saveFile = (data: any, fileName: string, type?: string, endings?: string): void => {
  // const options: BlobPropertyBag = {};
  // if (!!type)     {options.type = type; }
  // if (!!endings)  {options.endings = endings; }
  const file: File = new File([data], fileName);
  saveAs(file, fileName);
};
