import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-title',
  templateUrl: './title.component.html',
  styleUrls: ['./title.component.scss']
})
export class TitleComponent implements OnInit {
  @Input() title = '';
  @Input() iconClass = 'fa fa-user';
  @Input() subtitle = 'Desde 2023';
  @Input() btnListar = false;
  constructor() { }

  ngOnInit(): void {
  }

}
