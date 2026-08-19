import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoteDetalheComponent } from './lote-detalhe.component';

describe('LoteDetalheComponent', () => {
  let component: LoteDetalheComponent;
  let fixture: ComponentFixture<LoteDetalheComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LoteDetalheComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LoteDetalheComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
