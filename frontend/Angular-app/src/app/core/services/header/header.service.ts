import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { EnumButtonHeader } from 'src/app/shared/enums/EnumButtonHeader';

@Injectable({
  providedIn: 'root'
})
export class HeaderService {
  public buttonHeaderSubject = new Subject<EnumButtonHeader>();
  constructor() {

  }
}
