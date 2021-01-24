using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableFindBackend.Models;

namespace TableFindBackend.TypeConverters
{
    public class ReservationConverter: TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is string)
            {
                string[] parts = ((string)value).Split(new char[] { ',' });
                Reservation reservation = new Reservation();
                reservation.objectId = parts[0];
                reservation.TableId = parts[1];
                reservation.TakenFrom = DateTime.Parse(parts[2]);
                reservation.TakenTo = DateTime.Parse(parts[3]);
                reservation.UserId = parts[4];
                reservation.Number = parts[5];
                reservation.Name = parts[6];
                reservation.RestaurantId = parts[7];
                return reservation;
            }
            return base.ConvertFrom(context, culture, value);
        }
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                Reservation reservation = value as Reservation;
                return string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", reservation.objectId, reservation.TableId, reservation.TakenFrom, reservation.TakenTo, reservation.UserId, reservation.Number, reservation.Name, reservation.RestaurantId);
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }


    }
}
