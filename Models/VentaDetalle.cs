using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace CafeteriaApp.Models
{
    public class VentaDetalle : INotifyPropertyChanged
    {
        private int _cantidad;
        private double _precioUnitario;

        public int ProductoId { get; set; }
        public string NombreProducto { get; set; }

        public int Cantidad
        {
            get => _cantidad;
            set
            {
                if (_cantidad != value)
                {
                    _cantidad = value;
                    OnPropertyChanged(nameof(Cantidad));
                    OnPropertyChanged(nameof(Total));
                }
            }
        }

        public double PrecioUnitario
        {
            get => _precioUnitario;
            set
            {
                if (_precioUnitario != value)
                {
                    _precioUnitario = value;
                    OnPropertyChanged(nameof(PrecioUnitario));
                    OnPropertyChanged(nameof(Total));
                }
            }
        }

        public double Total => Cantidad * PrecioUnitario;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string nombreProp)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombreProp));
        }
    }
}
