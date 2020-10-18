
export interface Flights {
  departureDate: string,
  departureStation: string,
  arrivalStation: string,
  transport: {
    flightNumber: string
  },
  price: string,
  currency: string
}
