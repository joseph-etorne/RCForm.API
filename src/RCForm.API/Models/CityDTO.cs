using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCForm.API.Models
{
    public class CityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int NumberOfPointsOfInterest { get
            {
                return PointsOfInterest.Count;
            }
        }

        public ICollection<PointOfInterestDTO> PointsOfInterest { get; set; } = new List<PointOfInterestDTO>();
    }
}
