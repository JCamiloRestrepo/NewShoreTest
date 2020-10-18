import { Component, OnInit, Inject } from '@angular/core';
import { ImplicitReceiver } from '@angular/compiler';
import { HttpClient } from '@angular/common/http';
import { FlightService } from '../service/flight.service';

declare const ShowCarousel: any;
declare const ShowSlider: any;

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  public lstFlight: string[] = ["hola", "cara de bola", "pirinola", "otro"];

  bandera = false;

  constructor(http: HttpClient, @Inject("BASE_URL") baseUrl: string,
    protected flightService: FlightService
  ) {
  }

  ShowSliderHome() {
    ShowSlider();
  }

  ShowCarouselHome() {
    ShowCarousel();
  }

  public ShowTable(estado) {
    if (estado) {
      this.bandera = estado;
    }
    else {
      this.bandera = estado;
    }
      
  }
}

interface Search {

}
