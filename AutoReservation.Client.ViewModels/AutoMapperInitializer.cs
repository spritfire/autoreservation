using AutoMapper;
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
            Mapper.Initialize(cfg => cfg.CreateMap<KundeViewModel, KundeDto>());
            Mapper.Initialize(cfg => cfg.CreateMap<AutoViewModel, AutoDto>());
            Mapper.Initialize(cfg => cfg.CreateMap<ReservationViewModel, ReservationDto>());
        }
    }
}
