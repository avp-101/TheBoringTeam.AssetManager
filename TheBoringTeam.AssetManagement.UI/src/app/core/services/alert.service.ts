import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AlertService {

  constructor() { }

  public ShowSuccess(text: string) {
    const toast = swal.mixin({
      toast: true,
      position: 'top-end',
      showConfirmButton: false,
      timer: 2000
    });
    
    toast({
      type: 'success',
      title: text
    });
  }

  public ShowError(text: string) {
    const toast = swal.mixin({
      toast: true,
      position: 'top-end',
      showConfirmButton: false,
      timer: 2000
    });
    
    toast({
      type: 'error',
      title: text
    });
  }
}
