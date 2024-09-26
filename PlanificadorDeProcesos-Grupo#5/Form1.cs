using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Diseño realizado por Juan Carlos y Alejandro Velásquez
// Funcionamiento y lógica realizado por Abel Sanchez y Alvaro Flores

namespace PlanificadorDeProcesos_Grupo_5
{
    // Definición del formulario principal que hereda de MetroForm (una clase de MetroFramework para interfaces modernas)
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        // Constructor del formulario
        public Form1()
        {
            InitializeComponent(); // Inicializa los componentes de la interfaz
            UpdateProcessList(); // Llama a la función para actualizar la lista de procesos al cargar el formulario
            // Asigna el evento SelectionChanged al DataGridView para actualizar la información del proceso seleccionado
            dataGridView2.SelectionChanged += new EventHandler(dataGridView2_SelectionChanged);
        }

        // Función que actualiza la lista de procesos en el DataGridView
        private void UpdateProcessList()
        {
            dataGridView2.Rows.Clear(); // Limpia todas las filas del DataGridView
            // Itera sobre todos los procesos en ejecución en el sistema
            foreach (Process p in Process.GetProcesses())
            {
                // Añade una nueva fila al DataGridView
                int n = dataGridView2.Rows.Add();
                // Coloca el nombre del proceso en la primera celda de la fila actual
                dataGridView2.Rows[n].Cells[0].Value = p.ProcessName;
                // Coloca el ID del proceso en la segunda celda
                dataGridView2.Rows[n].Cells[1].Value = p.Id;
                // Convierte la memoria física en uso a MB y la coloca en la tercera celda
                double memoriaFisicaMB = p.WorkingSet64 / 1048576.0;
                dataGridView2.Rows[n].Cells[2].Value = memoriaFisicaMB.ToString("N2") + " MB";
                // Coloca la memoria virtual del proceso en la cuarta celda
                dataGridView2.Rows[n].Cells[3].Value = p.VirtualMemorySize64;
                // Coloca el ID de la sesión del proceso en la quinta celda
                dataGridView2.Rows[n].Cells[4].Value = p.SessionId;
            }
            // Actualiza el texto del contador de procesos con el número de filas (procesos) en el DataGridView
            txtContador.Text = "Procesos actuales: " + dataGridView2.Rows.Count.ToString();
        }

        // Evento que se dispara cuando el formulario carga
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        // Evento que se dispara al hacer clic en el label2 (parece que este evento está vacío)
        private void label2_Click(object sender, EventArgs e)
        {
        }

        // Evento que se dispara al hacer clic en una celda del DataGridView
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Coloca el nombre del proceso seleccionado en un cuadro de texto (txtProceso)
            txtProceso.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
        }

        // Evento que se dispara cuando cambia la selección de filas en el DataGridView
        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            // Verifica que haya una fila seleccionada y que no esté vacía
            if (dataGridView2.CurrentRow != null && dataGridView2.CurrentRow.Cells[0].Value != null)
            {
                // Actualiza el cuadro de texto con el nombre del proceso seleccionado
                txtProceso.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            }
        }

        // Evento que se dispara al hacer clic en el botón de actualizar
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            UpdateProcessList(); // Vuelve a cargar la lista de procesos
        }

        // Evento que se dispara al hacer clic en el label3 (parece que este evento también está vacío)
        private void label3_Click(object sender, EventArgs e)
        {
        }

        // Evento que se dispara al hacer clic en el botón de detener proceso
        private void btnDetener_Click(object sender, EventArgs e)
        {
            try
            {
                // Itera sobre todos los procesos en ejecución en el sistema
                foreach (Process p in Process.GetProcesses())
                {
                    // Si el nombre del proceso coincide con el seleccionado en el cuadro de texto (txtProceso)
                    if (p.ProcessName == txtProceso.Text)
                    {
                        p.Kill(); // Termina el proceso
                    }
                }
            }
            // Maneja excepciones si ocurre un error al intentar eliminar un proceso
            catch (Exception x)
            {
                // Muestra un mensaje de error en un cuadro de diálogo
                MessageBox.Show("No se seleccionó ningún proceso" + x, "Error al eliminar", MessageBoxButtons.OK);
            }
        }

        // Evento que se dispara al hacer clic en el botón de salir
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Cierra la aplicación
        }
    }
}
