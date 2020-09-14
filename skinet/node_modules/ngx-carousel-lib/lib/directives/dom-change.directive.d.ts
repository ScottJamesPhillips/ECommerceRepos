import { ElementRef, EventEmitter, OnDestroy } from '@angular/core';
export declare class DomChangeDirective implements OnDestroy {
    private elementRef;
    private changes;
    domChange: EventEmitter<{}>;
    constructor(elementRef: ElementRef);
    ngOnDestroy(): void;
}
