import { Component, OnInit, Inject, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FlightService } from '../service/flight.service';
import { Flights, cities } from '../Interfaces';
import DateTimeFormat = Intl.DateTimeFormat;
import { IDropdownSettings } from 'ng-multiselect-dropdown';

declare const ShowCarousel: any;
declare const ShowSlider: any;

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

  public origin: cities[];
  public destination: cities[];
  public flightDate: string;
  public minDate: Date;

  lstcities: cities[];
  citiesSettings: IDropdownSettings;

  public lstFlight: Flights[] = [];
  public lstFlightDB: Flights[] = [];
  flag = false;

  constructor(http: HttpClient, @Inject("BASE_URL") baseUrl: string,
    protected flightService: FlightService
  ) {
    this.minDate = new Date();
  }

  ngOnInit() {
    this.lstcities = [
      { item_id: "MDE", item_text: 'Medellin-MDE' },
      { item_id: "BOG", item_text: 'Bogotá-BOG' },
      { item_id: "CTG", item_text: 'Cartagena-CTG' },
      { item_id: "PEI", item_text: 'Pereira-PEI' }
    ];


    this.citiesSettings = {
      singleSelection: true,
      idField: 'item_id',
      textField: 'item_text',
      itemsShowLimit: 1
    };
  }


  ShowSliderHome() {
    ShowSlider();
  }

  ShowCarouselHome() {
    ShowCarousel();
  }

  public ShowTable(status) {
    if (status) {
      this.flag = status;
    }
    else {
      this.flag = status;
    }
  }

  public GetFlights() {
    if (this.origin.length <= 0 && this.destination.length == 1) {
      this.lstFlight = [];
      this.ShowTable(false);
      alert("The departure field is empty")
      this.flightService.GetFlight(null, this.destination[0].item_id, this.flightDate).subscribe(data => {        
        console.log(data);
        this.lstFlight = data;        
      });
    }
    else if (this.destination.length <= 0 && this.origin.length == 1) {
      this.lstFlight = [];
      this.ShowTable(false);
      alert("The destination field is empty")
      this.flightService.GetFlight(this.origin[0].item_id, null, this.flightDate).subscribe(data => {
        console.log(data);
        this.lstFlight = data;
      });
    }
    else if (this.destination.length <= 0 && this.origin.length == 0) {
      this.lstFlight = [];
      this.ShowTable(false);
      alert("The departure and destination fields are empty")
      this.flightService.GetFlight(null, null, this.flightDate).subscribe(data => {
        console.log(data);
        this.lstFlight = data;
      });
    }
    else {
      this.lstFlight = [];
      this.ShowTable(false);
      this.flightService.GetFlight(this.origin[0].item_id, this.destination[0].item_id, this.flightDate).subscribe(data => {
        console.log(data);
        this.lstFlight = data;
        this.ShowTable(true);
      });
    }
  }

  public SaveFlight(flight: Flights) {
    this.flightService.SaveFlight(flight).subscribe(data => {
      alert("The flight was successfully saved")
      this.lstFlight = [];
      this.ShowTable(false);
    });
  }

  public GetFlightsFromDB() {
    this.flightService.GetFlightsFromDB().subscribe(data => {
      this.lstFlightDB = data;
    })
  }
}




