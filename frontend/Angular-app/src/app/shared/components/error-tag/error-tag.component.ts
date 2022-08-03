import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-error-tag',
  templateUrl: './error-tag.component.html',
  styleUrls: ['./error-tag.component.scss']
})
export class ErrorTagComponent implements OnInit {
  @Input() msg!: string ;

  constructor() { }

  ngOnInit(): void {
  }

}
