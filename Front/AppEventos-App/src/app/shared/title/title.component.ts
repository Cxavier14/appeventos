import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

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

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  listar(): void {
    this.router.navigate([`/${this.title.toLowerCase()}/lista`])
  }

}
