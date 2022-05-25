import {ICellEditorAngularComp} from 'ag-grid-angular';
import {AfterViewInit, Component, Input, ViewChild} from '@angular/core';
import {Dropdown} from 'primeng/dropdown';

@Component({
    selector: 'dropdown-cell-editor',
    template: `<p-dropdown #input
        [options]="options"
        [optionLabel]="optionLabel"
        [style]="{width:'100%', border: 'none', lineHeight: 0}"
        [(ngModel)]="value"
        appendTo="body"
        [showClear]="true"
        [autoDisplayFirst]="false"
        [editable]="editable"
        (keydown)="onKeyDown($event)"
        (onChange)="onChange($event)"></p-dropdown>`
})
export class DropdownCellEditorComponent implements ICellEditorAngularComp, AfterViewInit {
    private uppercase;

    @ViewChild('input') input: Dropdown;
    public value;

    @Input() options;
    @Input() optionLabel;
    @Input() editable;

    agInit(params: any): void {
        this.options = params.options;
        this.optionLabel = params.optionLabel;
        this.value = params.value;
        this.editable = params.editable;
        this.uppercase = params.uppercase;
    }

    ngAfterViewInit(): void {
        setTimeout(() => {
            this.input.focus();
        });
    }

    getValue() {
        if (this.uppercase && this.value) {
            this.value = this.value.toUpperCase();
        }
        return this.value;
    }

    onKeyDown(e) {
        if (e.keyCode !== 13 && e.keyCode !== 27) {
            e.stopPropagation();
        }
    }

    onChange(e) {
        if (e.originalEvent instanceof KeyboardEvent) {
            e.originalEvent.stopPropagation();
        }
    }
}
