import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-troubleshoot',
  templateUrl: './troubleshoot.component.html',
})
export class TroubleshootComponent implements OnInit{
  public stepText: string;
  public options: Option[];
  public nextStep: number;
  private baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
    this.nextStep = 1;
  }

  ngOnInit(): void {
    this.getNextStep();
  }

  getNextStep() {
    console.log('step id is = ' + this.nextStep);
    this.http.get<Step>(this.baseUrl + 'steps/' + this.nextStep).subscribe(result => {
        this.stepText = result.text;
        this.options = result.options;
    },
    error => console.error(error));
  }

  onItemChange(value) {
    this.nextStep = value;
  }
}

interface Step {
  text: string;
  options: Option[];
}

interface Option {
  text: string;
  nextStep: number;
}
