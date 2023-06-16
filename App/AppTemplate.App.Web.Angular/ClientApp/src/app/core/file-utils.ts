import { HttpResponse } from '@angular/common/http';
import  * as FileSaver from 'file-saver';

const getFileNameFromContentDisposition = (response: HttpResponse<Blob>) => {
  const header = response.headers.get('Content-Disposition');
  const result = header!.split(';')[1].trim().split('=')[1];
  return result.replace(/"/g, '');
}

export const saveFile = (response: HttpResponse<Blob>, fileName?: string) =>
{
  const file = fileName || getFileNameFromContentDisposition(response);
  FileSaver.saveAs(response.body!, file);
}

