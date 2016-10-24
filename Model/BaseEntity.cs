using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaberSistemi.Data.Model
{
    public class BaseEntity
    {
        [Key]
        public int ID { get; set; }

        private DateTime Tarih = DateTime.Now;

        private bool Aktif = true;

        public DateTime EklenmeTarihi { get {
                return Tarih;
            } set {
                Tarih = value;
            } }

        public bool AktifMi { get
            {
                return Aktif;

            }
            set
            {
                Aktif = value;
                    }
        }
    }
}
