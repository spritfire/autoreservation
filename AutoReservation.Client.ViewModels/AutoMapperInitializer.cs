using AutoMapper;
using AutoReservation.Client.ViewModels.ViewModels;
using AutoReservation.Common.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.Client.ViewModels
{
    class AutoMapperInitializer
    {
        public void InitializeAutoMapper()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<KundeDetailViewModel, KundeDto>());
            Mapper.Initialize(cfg => cfg.CreateMap<AutoDetailViewModel, AutoDto>());
            Mapper.Initialize(cfg => cfg.CreateMap<ReservationDetailViewModel, ReservationDto>());
        }
    }
}
