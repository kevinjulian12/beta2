﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Domain;

namespace Beta
{
    public partial class HistorialDeComprasClientes : Form
    {
        public HistorialDeComprasClientes()
        {
            InitializeComponent();
        }
        CN_Ventas ventas = new CN_Ventas();
        CN_VentasItem VentasItem = new CN_VentasItem();

        public void mostrar()
        {
            dataGridView1.DataSource = ventas.MostraHistVent();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
        }

        private void HistorialDeComprasClientes_Load(object sender, EventArgs e)
        {
            mostrar();
        }
        string NombreColumna = "";
        private void FiltrarDatosDatagridview(DataGridView datagrid, string nombre_columna, TextBox txt_buscar)
        {
            ///Al texto recibido si contiene un asterisco (*) lo reemplazo de la cadena
            ///para que no provoque una excepción.
            string cadena = txt_buscar.Text.Trim().Replace("*", "");
            string filtro = string.Format("convert([{0}], System.String) LIKE '{1}%'", nombre_columna, cadena);

            ///A la vista del DataGridView con la propiedad RowFilter
            ///se le asigna la cadena del filtro para mostrarla en el DataGridView
            (datagrid.DataSource as DataTable).DefaultView.RowFilter = filtro;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            FiltrarDatosDatagridview(dataGridView1, NombreColumna, textBox1);
        }
        private void tuGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            NombreColumna = dataGridView1.Columns[e.ColumnIndex].DataPropertyName.Trim();
            textBox1.Enabled = true;
            label6.Visible = false;

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    ventas.idVenta = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                    VentasItem.idVenta = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                    VentasItem.Eliminar();
                    ventas.Eliminar();
                    MessageBox.Show("Eliminado correctamente");
                    mostrar();
                }
                else
                    MessageBox.Show("seleccione una fila por favor");
            }
            catch (Exception)
            {
                MessageBox.Show("No hay un registro seleccionado");
            }

        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0) {
                    FormDetalleVenta formDetalleVenta = new FormDetalleVenta();
                    VentasItem.idVenta = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                    formDetalleVenta.ShowDialog();
                }
                else
                    MessageBox.Show("Por favor seleccione una fila...");
            }
            catch (Exception)
            {

                MessageBox.Show("No hay un registro seleccionado");
            }
        }
    }
}
