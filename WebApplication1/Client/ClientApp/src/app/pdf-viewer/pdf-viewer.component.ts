import {Component} from '@angular/core';
import {MessageService} from 'primeng/api';
import {DomSanitizer} from '@angular/platform-browser';

@Component({
  selector: 'app-file-viewer',
  templateUrl: './pdf-viewer.component.html',
  styleUrls: ['./pdf-viewer.component.css'],
  providers: []
})

export class PdfViewerComponent{
  url: any;
  protected data: Blob;
  protected type: string;
  title: string;

  constructor(
    private messageService: MessageService,
    private sanitizer: DomSanitizer) {
  }

  async init(data: Blob, type: string, title: string) {
    this.data = data;
    this.type = type;
    this.title = title;

    const unsafeURL = window.URL.createObjectURL(data) + '#toolbar=0'; // works in Chrome, doesn't work in Firefox

    this.url =  this.sanitizer.bypassSecurityTrustResourceUrl(unsafeURL);

    // this.visible = true;
  }
}
