import { Directive, HostListener, ElementRef, Renderer2 } from '@angular/core';

@Directive({
  selector: '[appHover]'
})
export class HoverDirective {

  @HostListener('mouseover')
  onMouseOver() {
    this.renderer.addClass(this.el.nativeElement, 'hovered');
  }

  @HostListener('mouseout')
  onMouseOut() {
    this.renderer.removeClass(this.el.nativeElement, 'hovered');
  }

  constructor(private  el: ElementRef, private renderer: Renderer2) {}

}
