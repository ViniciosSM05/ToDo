import { AfterViewInit, Directive, ElementRef } from '@angular/core';

@Directive({
  selector: '[autofocus]'
})
export class AutoFocusDirective implements AfterViewInit {
  constructor(private elementRef: ElementRef) 
  { }

  ngAfterViewInit(): void {
      this.elementRef.nativeElement.focus();    
  }

}
