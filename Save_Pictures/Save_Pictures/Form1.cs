using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace Save_Pictures
{
    public partial class Form1 : Form
    {
        public static object Using { get; private set; }
        #region Events
        public Form1()
        {
            InitializeComponent();
        }

        private void btnPicture_Click(object sender, EventArgs e)
        {
            optenerRuta();
        }
        #endregion

        #region guardado de imagenes metodos
        // ------------ Optiene la ruta donde van a ser guardadas las imagenes
        public void optenerRuta()
        {
            //guardar solo un archivo
            //SaveFileDialog sfd = new SaveFileDialog();
            //sfd.InitialDirectory = @"D:\Pictures\";
            //sfd.RestoreDirectory = true;
            //sfd.FileName = "*.jpg";
            //sfd.DefaultExt = ".jpg";
            //sfd.Filter = "jpg files (*.jpg)|*.jpg";

            //if (sfd.ShowDialog() == DialogResult.OK)
            //{
            //   SaveProcess(sfd.OpenFile().ToString());
            //}
            // selecciona una carpeta
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Seleccione la ruta";
            fbd.RootFolder = Environment.SpecialFolder.Desktop;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                SaveProcess(fbd.SelectedPath.ToString()+"\\");
            }
        }
        //-----------------combierte los bytes almacenados en la base de datos y los guarda formato jpg
        private static void WriteBinaryFile(string fileName, byte[] data)
        {
            if ((string.IsNullOrEmpty(fileName)))
                throw new ArgumentException("No se ha especificado el archivo de destino.", "fileName");

            if ((data == null))
                throw new ArgumentException("Los datos no son válidos para crear un archivo.", "data");

            // Crear el archivo. Se producirá una excepción si ya existe
            // un archivo con el mismo nombre.
            using (FileStream fs = new FileStream(fileName, FileMode.CreateNew, FileAccess.Write))
            {
                // Crea el escritor para la secuencia.
                BinaryWriter bw = new BinaryWriter(fs);

                // Escribir los datos en la secuencia.
                bw.Write(data);
            }
        }

        // proceso de guardado de las imagenes
        public void SaveProcess(string urlSave)
        {
            byte[] data = null;
            try
            {
                // Construimos los correspondientes objetos para
                // conectarnos a la base de datos de SQL Server,
                // utilizando la seguridad integrada de Windows NT.

                using (SqlConnection cnn = new SqlConnection(@"Data Source =GMSQL\TRAVERSE11; initial catalog = TES; user id= sa; password = Osi-149016!; MultipleActiveResultSets = True;"))
                {
                    // Creamos un comando.
                    SqlCommand cmd = cnn.CreateCommand();

                    // Seleccionamos únicamente el campo que contiene
                    // los documentos, filtrándolo mediante su
                    // correspondiente campo identificador.

                    cmd.CommandText = "SELECT ISNULL(P.PictItem,'') FOTO, ISNULL(P.PictId,'') ID FROM tblInItemPict P WITH(NOLOCK)";// where PictId = \'001004 \'";

                    // Abrimos la conexión.
                    cnn.Open();

                    // Creamos un DataReader.
                    SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    // Leemos el registro.
                    int bufferSize, total_filas = 0;
                    pgBarCarga.Minimum = 0;
                    while (dr.Read())
                    {
                        pgBarCarga.Increment(1);
                        total_filas++;
                        // El tamaño del búfer debe ser el adecuado para poder
                        // escribir en el archivo todos los datos leídos.
                        //
                        // Si el parámetro 'buffer' lo pasamos como Null, obtendremos
                        // la longitud del campo en bytes.

                        bufferSize = Convert.ToInt32(dr.GetBytes(0, 0, null, 0, 0));

                        // Creamos el array de bytes. Como el índice está
                        // basado en cero, la longitud del array será la
                        // longitud del campo menos una unidad.
                        //
                        data = new byte[bufferSize];

                        // Leemos el campo.
                        //
                        dr.GetBytes(0, 0, data, 0, bufferSize);

                        // Procedemos a crear el archivo, en el ejemplo
                        // un documento de Microsoft Word.
                        WriteBinaryFile(urlSave + dr["ID"].ToString() + ".png", data);
                        // WriteBinaryFile("D:\\Pictures\\Imagenes_Productos\\" + dr["ID"].ToString() + ".jpg", data);
                    }
                    // Cerramos el objeto DataReader, e implícitamente la conexión.
                    dr.Close();
                    pgBarCarga.Maximum = total_filas;
                }
                pgBarCarga.Value = pgBarCarga.Maximum;
                MessageBox.Show("Archivos exportados correctamente.", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}
