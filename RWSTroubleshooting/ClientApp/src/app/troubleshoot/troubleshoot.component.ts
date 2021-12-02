import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-troubleshoot',
  templateUrl: './troubleshoot.component.html',
})
export class TroubleshootComponent {
    public currentStep: Step;

    constructor(private http: HttpClient) { }
    
    getNextStep() {
      this.http.get<Step>('http://localhost:5000/steps/1').subscribe(result => {
          this.currentStep = result;
      }, error => console.error(error));
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
