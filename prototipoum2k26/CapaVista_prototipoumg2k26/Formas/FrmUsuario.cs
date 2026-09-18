using System;
using System.Linq;
using System.Windows.Forms;
using CapaControlador_prototipoumg2k26;
using CapaVista_prototipoumg2k26.Reportes;

namespace CapaVista_prototipoumg2k26.Formas
{
    public partial class FrmUsuario : Form
    {
        private ModeloUsuario usuario = new ModeloUsuario();

        public FrmUsuario()
        {
            InitializeComponent();
            panIngresoDatos.Enabled = false;

            // Eventos para transformar y formatear la columna del estado
            dgvUsuario.DataBindingComplete += dgvUsuario_DataBindingComplete;
            dgvUsuario.CellFormatting += dgvUsuario_CellFormatting;

            CargarDatos();
        }

        private void FrmUsuario_Load(object sender, EventArgs e)
        {
            listaUsuarios();
        }

        // Reemplaza la columna CheckBox por una de Texto buscando por HeaderText o DataPropertyName
        private void dgvUsuario_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < dgvUsuario.Columns.Count; i++)
            {
                DataGridViewColumn col = dgvUsuario.Columns[i];

                if (col.DataPropertyName.Equals("EstadoUsuario", StringComparison.OrdinalIgnoreCase) ||
                    col.HeaderText.Equals("EstadoUsuario", StringComparison.OrdinalIgnoreCase))
                {
                    if (col is DataGridViewCheckBoxColumn)
                    {
                        int index = col.Index;
                        string dataProperty = col.DataPropertyName;
                        string headerText = col.HeaderText;

                        dgvUsuario.Columns.RemoveAt(index);

                        DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn
                        {
                            Name = "EstadoUsuarioText",
                            DataPropertyName = dataProperty,
                            HeaderText = headerText
                        };

                        dgvUsuario.Columns.Insert(index, textColumn);
                    }
                    break;
                }
            }
        }

        // Convierte los valores booleanos o numéricos (1/0, true/false) en "Activo" / "Inactivo"
        private void dgvUsuario_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.Value == null) return;

            DataGridViewColumn col = dgvUsuario.Columns[e.ColumnIndex];

            if (col.DataPropertyName.Equals("EstadoUsuario", StringComparison.OrdinalIgnoreCase) ||
                col.HeaderText.Equals("EstadoUsuario", StringComparison.OrdinalIgnoreCase))
            {
                string val = e.Value.ToString().Trim().ToLower();

                if (val == "true" || val == "1")
                {
                    e.Value = "Activo";
                    e.FormattingApplied = true;
                }
                else if (val == "false" || val == "0")
                {
                    e.Value = "Inactivo";
                    e.FormattingApplied = true;
                }
            }
        }

        private void listaUsuarios()
        {
            try
            {
                dgvUsuario.DataSource = usuario.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dgvUsuario.DataSource = null;
            dgvUsuario.DataSource = usuario.FindbyId(txtSearch.Text);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dgvUsuario.DataSource = null;
            dgvUsuario.DataSource = usuario.FindbyId(txtSearch.Text).ToList();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            usuario.NombreUsuario = txtNombre.Text;
            usuario.ContrasenaUsuario = txtContraseña.Text;

            var cboInterno = comboI1.Controls.OfType<ComboBox>().FirstOrDefault();
            if (cboInterno != null && cboInterno.SelectedValue != null)
            {
                usuario.IdRolUsuario = Convert.ToInt32(cboInterno.SelectedValue);
            }

            bool valido = new Ayudas.ValidacionDatos(usuario).Validar();

            if (valido == true)
            {
                string resultado = usuario.GrabarCambios();

                MessageBox.Show(resultado);

                listaUsuarios();

                Reinicio();
            }
        }

        private void Reinicio()
        {
            panIngresoDatos.Enabled = false;

            txtNombre.Clear();
            txtContraseña.Clear();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            panIngresoDatos.Enabled = true;

            usuario.Estado = EstadoEntidad.Added;

            txtNombre.Clear();
            txtContraseña.Clear();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvUsuario.SelectedRows.Count > 0 && dgvUsuario.CurrentRow != null)
            {
                panIngresoDatos.Enabled = true;

                usuario.Estado = EstadoEntidad.Modified;

                if (dgvUsuario.CurrentRow.Cells[0].Value != null)
                {
                    usuario.IdPK = Convert.ToInt32(dgvUsuario.CurrentRow.Cells[0].Value);
                }

                if (dgvUsuario.CurrentRow.Cells[1].Value != null)
                {
                    txtNombre.Text = dgvUsuario.CurrentRow.Cells[1].Value.ToString();
                }

                if (dgvUsuario.CurrentRow.Cells[2].Value != null)
                {
                    txtContraseña.Text = dgvUsuario.CurrentRow.Cells[2].Value.ToString();
                }
            }
            else
            {
                MessageBox.Show("Seleccione una fila");
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (dgvUsuario.SelectedRows.Count > 0 && dgvUsuario.CurrentRow != null)
            {
                usuario.Estado = EstadoEntidad.Deleted;

                if (dgvUsuario.CurrentRow.Cells[0].Value != null)
                {
                    usuario.IdPK = Convert.ToInt32(dgvUsuario.CurrentRow.Cells[0].Value);
                }

                string resultado = usuario.GrabarCambios();

                MessageBox.Show(resultado);

                listaUsuarios();
            }
            else
            {
                MessageBox.Show("Seleccione una fila");
            }
        }

        private void CargarDatos()
        {
            comboI1.llenarCombo(
                "Tbl_Rol",
                "IdRol",
                "NombreRol"
            );
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            //Muestra el formulario de reporte de usuario 
            frmReporteUsuario reporte = new frmReporteUsuario();
            reporte.Show();
        }
    }
}