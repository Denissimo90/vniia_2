import {Injectable} from '@angular/core';
import {Document, DocumentObject} from '../domain/Document';
import {AuthService, EventBusService, FileViewerService, PageQuery, PageResponse, ServeFileType} from '@prism/common';
import {DocumentCategory} from '../domain/DocumentCategory';
import {DocumentCode} from '../domain/DocumentCode';
import {DocumentSheet} from '../domain/DocumentSheet';
import {BaseService} from './base.service';
import {HttpClient, HttpParams} from '@angular/common/http';
import {GroupElement} from '../domain/GroupElement';
import {OperationType} from '../domain/Element';
import {Dataset} from '../domain/Dataset';

@Injectable()
export class DocumentsService extends BaseService {
  private restPath = 'documents';
  constructor(
    protected httpClient: HttpClient,
    protected authService: AuthService,
    protected bus: EventBusService,
    protected fileViewerService: FileViewerService) {
    super(httpClient, authService, bus);
  }

  async getDocs(query: PageQuery, archive: boolean, plm: boolean, showViewable: boolean)
      : Promise<PageResponse<Document>> {
    return this.http.postPageQuery<Document>(`/${this.restPath}/search`, query,
      {
        params: new HttpParams()
          .set('archive', archive.toString())
          .set('plm', plm.toString())
          .set('showViewable', showViewable.toString())
      }
    ).toPromise();
  }

  async getCategories(): Promise<DocumentCategory[]> {
    const categories = await this.http
      .get<DocumentCategory[]>(`/${this.restPath}/categories`, this.httpOptions)
      .toPromise();
    console.log('fetched element categories');
    return categories;
  }

  async searchCodes(from: number, count: number, sorts: any, filters: any): Promise<{ items: DocumentCode[], total: number }> {
    const codes = await this.http.post<{ items: DocumentCode[], total: number }>(`/${this.restPath}/codes/search`,
      {
        first: from,
        rows: count,
        filters: filters,
        sorts: sorts
      },
      {
        params: new HttpParams()
          .set('archive', 'false')

      }
    ).toPromise();
    return codes;
  }

  async getDocumentCodes(query: PageQuery, archive: boolean): Promise<PageResponse<DocumentCode>> {
    return this.http.postPageQuery<DocumentCode>(`/${this.restPath}/codes/search`, query,
      {
        params: new HttpParams()
          .set('archive', archive.toString())

      }
    ).toPromise();
  }

  async postDocumentCode(code: DocumentCode): Promise<DocumentCode> {
    const promise = await this.http.post<DocumentCode>(`/${this.restPath}/codes`, code).toPromise();
    if (promise == null) { throw new Error('error posting album'); }
    Object.assign(code, promise);
    console.log(`DocumentCode added`);
    return promise;
  }

  async putDocumentCode(code: DocumentCode): Promise<DocumentCode> {
    const promise = await this.http.put<DocumentCode>(`/${this.restPath}/codes/${code.id}`, code,
      {
        params: new HttpParams()
      }).toPromise();
    console.log('DocumentCode edited');
    return promise;
  }

  async archiveDocumentCode(code: DocumentCode): Promise<void> {
    await this.http.put(`/${this.restPath}/codes/${code.id}/archive`, code,
      {
        params: new HttpParams()
      }).toPromise();
    console.log(`DocumentCode edited and archived`);
  }

  async getTypes(): Promise<DocumentType[]> {
    const types = await this.http.get<DocumentType[]>(`/${this.restPath}/types`, this.httpOptions)
      .toPromise();
    console.log('fetched element types');
    return types;
  }

  getDocument(id: number): Promise<Document> {
    return this.http.get<Document>(`/${this.restPath}/${id}`, this.httpOptions).toPromise();
  }

  async postDoc(document: Document): Promise<void> {
    const promise = await this.http.post<Document>(`/${this.restPath}/`, document).toPromise();
    if (promise == null) { throw new Error('error posting document'); }
    Object.assign(document, promise);
    console.log(`added document`);
  }

  async putDoc (document: Document, archive = false): Promise<Document> {
    return await this.http.put<Document>(`/${this.restPath}/${document.id}`, document,
      {
        params: new HttpParams()
          .set('archive', archive.toString())
      }).toPromise();
    console.log(archive ? `document edited and archived` : 'document edited');
  }

  async reissueDoc(document: Document): Promise<void> {
    const promise = await this.http.put(`/${this.restPath}/${document.id}/reissue`, document).toPromise();
    if (promise == null) { throw new Error('error reissued document'); }
    Object.assign(document, promise);
    console.log(`document reissued`);
  }

  groupReissue(ids: number[], groupElement: GroupElement, operationType: OperationType): Promise<void> {
    return this.http.put<void>(`/${this.restPath}/group/${ids.join(',')}/${OperationType[operationType].toLowerCase()}`, groupElement).toPromise();
  }

  async getDocumentSheetsCount(documentSheet: DocumentSheet[]): Promise<number> {
    const count = await this.http.post<number>(`/${this.restPath}/sheetscount`, documentSheet).toPromise();
    console.log('fetched sheets count');
    return count;
  }

  async acceptPlmDocument(document: Document): Promise<void> {
    const promise = await this.http.put(`/${this.restPath}/${document.id}/accept`, null).toPromise();
    // if (promise == null) { throw new Error('error to accept plm document'); }
    // Object.assign(document, promise);
    console.log(`plm document accepted`);
  }

  async isDocumentFileInPlmExists(document: Document): Promise<boolean> {
    const exist = await this.http.post<boolean>(`/${this.restPath}/fileexists`, document).toPromise();
    return exist;
  }

  async getDigitPaperRation(year: number): Promise<Map<DocumentObject, Number>> {
    return await this.http.get<Map<DocumentObject, Number>>(`/${this.restPath}/${year}/digit-paper-ratio`).toPromise();
  }

  async getBarData(key: String, year: number, quarter: number): Promise<Dataset[]> {
    return await this.http.get<Dataset[]>(`/${this.restPath}/${year}/${quarter}/${key}-data`).toPromise();
  }

  async getBarFile(key: String, year: number, quarter: number, type: String): Promise<void> {

    const data = await this.http.get(`/${this.restPath}/${year}/${quarter}/${key}-data/file/${type}`, {observe: 'response', responseType: 'blob'}).toPromise();
    await this.fileViewerService.showOrSaveFile(data, type === 'pdf' ? ServeFileType.VIEW : ServeFileType.SAVE);

  }

  async getPieFile(year: any, type: string) {

    const data = await this.http.get(`/${this.restPath}/${year}/digit-paper-ratio/file/${type}`, {observe: 'response', responseType: 'blob'}).toPromise();

    await this.fileViewerService.showOrSaveFile(data, type === 'pdf' ? ServeFileType.VIEW : ServeFileType.SAVE);

  }
}
