using Agenda_lenguaje.BLL;
using Agenda_lenguaje.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agenda_lenguaje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Limpiar()
        {
            IdnumericUpDown.Value = 0;
            txtNombre.Text = string.Empty;
            txtApellidos.Text = string.Empty;
            txtTelf.Text = string.Empty;
            txtCell.Text = string.Empty;
            MyErrorProvider.Clear();
        }
        private void Limpiar2()
        {
            IdEventonumericUpDown.Value = 0;
            DescripciontextBox.Text = string.Empty;
            FechadateTimePicker.Text = string.Empty;   
            MyErrorProvider.Clear();
        }


        private Agenda LlenaClase()
        {
            Agenda agenda = new Agenda()
            {
                Id = (int)IdnumericUpDown.Value,
                Nombre = txtNombre.Text,
                Apellido = txtApellidos.Text,
                Telefono = txtTelf.Text,
                Celular = txtCell.Text,
                
            };
            return agenda;
        }

        private Eventos LlenaClase2()
        {
            Eventos eventos = new Eventos()
            {
                IdEvento = (int)IdEventonumericUpDown.Value,
                Descripcion = DescripciontextBox.Text,
                Fecha = FechadateTimePicker.Value,
            };
            return eventos;
        }

        private void LlenaCampo(Agenda agenda)
        {
            IdnumericUpDown.Value = agenda.Id;
            txtNombre.Text = agenda.Nombre;
            txtApellidos.Text = agenda.Apellido;
            txtTelf.Text = agenda.Telefono;
            txtCell.Text = agenda.Celular;   
        }

        private void LlenaCampo2(Eventos eventos)
        {
            IdEventonumericUpDown.Value = eventos.IdEvento;
            DescripciontextBox.Text = eventos.Descripcion;
            FechadateTimePicker.Value = eventos.Fecha;
        }


        private bool ExisteEnLaBaseDeDatos()
        {
            RepositorioBase<Agenda> repositorio = new RepositorioBase<Agenda>();
            Agenda agenda = repositorio.Buscar((int)IdnumericUpDown.Value);
            return (agenda != null);
        }

        private bool ExisteEnLaBaseDeDatos2()
        {
            RepositorioBase<Eventos> repositorio = new RepositorioBase<Eventos>();
            Eventos eventos = repositorio.Buscar((int)IdEventonumericUpDown.Value);
            return (eventos != null);
        }

        private bool Validar()
        {
            bool paso = true;
            MyErrorProvider.Clear();

            if (txtNombre.Text == string.Empty)
            {
                MyErrorProvider.SetError(txtNombre, "El campo Nombre no puede estar vacio");
                txtNombre.Focus();
                paso = false;
            }
            if (txtApellidos.Text == string.Empty)
            {
                MyErrorProvider.SetError(txtApellidos, "El campo apellido no puede estar vacio");
                txtApellidos.Focus();
                paso = false;
            }
            if (txtTelf.Text == string.Empty)
            {
                MyErrorProvider.SetError(txtTelf, "El campo telefono no puede estar vacio");
                txtTelf.Focus();
                paso = false;
            }
            if (txtCell.Text == string.Empty)
            {
                MyErrorProvider.SetError(txtCell, "El campo celular no puede estar vacio");
                txtCell.Focus();
                paso = false;
            }


            return paso;
        }

        private bool Validar2()
        {
            bool paso = true;
            MyErrorProvider.Clear();

            if (DescripciontextBox.Text == string.Empty)
            {
                MyErrorProvider.SetError(DescripciontextBox, "El campo Descripcion no puede estar vacio");
                DescripciontextBox.Focus();
                paso = false;
            }
         
            return paso;
        }

        private void Guardar_Click(object sender, EventArgs e)
        {
            RepositorioBase<Agenda> repositorio = new RepositorioBase<Agenda>();

            Agenda agenda = new Agenda();
            bool paso = false;

            if (!Validar())
                return;

            agenda = LlenaClase();

            if (IdnumericUpDown.Value == 0)
            {
                paso = repositorio.Guardar(agenda);
                Limpiar();
            }

            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    MessageBox.Show("No se puede modificar un contacto que no existe", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                {
                    paso = repositorio.Modificar(agenda);
                    MessageBox.Show("Contacto Modificado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }

            if (paso)
            {
                MessageBox.Show("Guardado!!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
            }

            else
                MessageBox.Show("No fue posible guardar!!", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Agenda> repositorio = new RepositorioBase<Agenda>();
            Agenda agenda = new Agenda();

            int.TryParse(IdnumericUpDown.Text, out int id);

            agenda = repositorio.Buscar(id);

            if (agenda != null)
            {
                MyErrorProvider.Clear();
                LlenaCampo(agenda);
            }
            else
            {
                Limpiar();
                MyErrorProvider.SetError(IdnumericUpDown, "Contacto no Encontrado");
              
            }
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            RepositorioBase<Agenda> repositorio = new RepositorioBase<Agenda>();

                MyErrorProvider.Clear();
                int.TryParse(IdnumericUpDown.Text, out int id);

                if (!ExisteEnLaBaseDeDatos())
                {
                    MyErrorProvider.SetError(IdnumericUpDown, "Contacto No Existe!!!");
                    return;
                }
                if (repositorio.Eliminar(id))
                {
                    Limpiar();
                    MessageBox.Show("Contacto Eliminado!!", "Exito!!!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
          
        }

        private void Nuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Limpiar2();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RepositorioBase<Eventos> repositorio = new RepositorioBase<Eventos>();

            Eventos eventos = new Eventos();
            bool paso = false;

            if (!Validar2())
                return;

            eventos = LlenaClase2();

            if (IdEventonumericUpDown.Value == 0)
            {
                paso = repositorio.Guardar(eventos);
                Limpiar2();
            }

            else
            {
                if (!ExisteEnLaBaseDeDatos2())
                {
                    MessageBox.Show("No se puede modificar un evento que no existe", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                {
                    paso = repositorio.Modificar(eventos);
                    MessageBox.Show("Evento Modificado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }

            if (paso)
            {
                MessageBox.Show("Guardado!!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar2();
            }

            else
                MessageBox.Show("No fue posible guardar!!", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RepositorioBase<Eventos> repositorio = new RepositorioBase<Eventos>();

            MyErrorProvider.Clear();
            int.TryParse(IdEventonumericUpDown.Text, out int id);

            if (!ExisteEnLaBaseDeDatos2())
            {
                MyErrorProvider.SetError(IdEventonumericUpDown, "Evento No Existe!!!");
                return;
            }
            if (repositorio.Eliminar(id))
            {
                Limpiar2();
                MessageBox.Show("Evento Eliminado!!", "Exito!!!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RepositorioBase<Eventos> repositorio = new RepositorioBase<Eventos>();
            Eventos eventos = new Eventos();

            int.TryParse(IdEventonumericUpDown.Text, out int id);

            eventos = repositorio.Buscar(id);

            if (eventos != null)
            {
                MyErrorProvider.Clear();
                LlenaCampo2(eventos);
            }
            else
            {
                Limpiar2();
                MyErrorProvider.SetError(IdEventonumericUpDown, "Evento no Encontrado");
            }
        }
    }
}
