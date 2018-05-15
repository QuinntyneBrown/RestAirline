﻿using System;
using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.Controllers;
using RestAirline.Api.HyperMedia;
using RestAirline.Api.Resources.Availability;
using RestAirline.Api.Resources.Booking.Seat;

namespace RestAirline.Api.Resources.Booking
{
    public class SelectTripResultResource
    {
        [Obsolete("For serialization")]
        public SelectTripResultResource() { }

        public SelectTripResultResource(IUrlHelper urlHelper, Guid bookingId)
        {
            ResourceLinks = new Links(urlHelper, bookingId);
            ResourceCommands = new Commands(urlHelper);
        }

        public Guid BookingId { get; set; }
        public Links ResourceLinks { get; set; }
        public Commands ResourceCommands { get; set; }

        public class Links
        {
            private readonly IUrlHelper _urlHelper;
            private readonly Guid _bookingId;

            public Links(IUrlHelper urlHelper, Guid bookingId)
            {
                _urlHelper = urlHelper;
                _bookingId = bookingId;
            }

            public Link<SelectTripResultResource> Self => _urlHelper.Link((BookingController c) => c.SelectTrip(null));
            public Link<TripAvailabilityResource> Parent => _urlHelper.Link((TripAvailabilityController c) => c.SearchTrips(null));
            public Link<BookingResource> Booking => _urlHelper.Link((BookingController c) => c.GetBooking(_bookingId));
        }

        public class Commands
        {
            private readonly IUrlHelper _urlHelper;

            public Commands(IUrlHelper urlHelper)
            {
                _urlHelper = urlHelper;
            }

            public ChangeFlightCommand ChangeFlight => new ChangeFlightCommand(_urlHelper);
            public AssignSeatCommand AssignSeat => new AssignSeatCommand(_urlHelper);
            public AssignSeatAutomaticallyCommand AssignSeatAutomaticallyCommand => new AssignSeatAutomaticallyCommand(null);
        }
    }
}