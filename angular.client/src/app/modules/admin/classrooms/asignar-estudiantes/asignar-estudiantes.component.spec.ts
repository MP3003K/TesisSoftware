import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AsignarEstudiantesComponent } from './asignar-estudiantes.component';

describe('AsignarEstudiantesComponent', () => {
  let component: AsignarEstudiantesComponent;
  let fixture: ComponentFixture<AsignarEstudiantesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AsignarEstudiantesComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AsignarEstudiantesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
