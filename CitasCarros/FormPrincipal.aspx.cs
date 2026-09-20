using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SQLite;
using System.Net.Mail;
using System.Net;

namespace CitasCarros
{
    public partial class FormPrincipal : System.Web.UI.Page
    {
        public int HoraEntrada = 8, HoraSalida = 9, Cosonio = 0;
        public string TipoSer = "", NombreCli = "";
        public class Cita
        {
            public int Id { get; set; }
            public string NombreCliente { get; set; }
            public string CorreoCliente { get; set; }
            public string ModeloCarro { get; set; }
            public DateTime FechaEntrada { get; set; }
            public TimeSpan HorarioEntrada { get; set; }
            public TimeSpan HorarioSalida { get; set; }
            public string TipoServicio { get; set; }
            public string Area { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtFechaEntrada.Text = DateTime.Today.ToString("yyyy-MM-dd");
                CargarTablaHorarios(DateTime.Today);
            }
            //if (Cosonio == 0)
            //{
            //    ddlHorarioEntrada.Items.Add("08:00");
            //    ddlHorarioEntrada.Items.Add("09:00");
            //    ddlHorarioEntrada.Items.Add("10:00");
            //    ddlHorarioEntrada.Items.Add("11:00");
            //    ddlHorarioEntrada.Items.Add("12:00");
            //    ddlHorarioEntrada.Items.Add("13:00");
            //    ddlHorarioEntrada.Items.Add("14:00");
            //    ddlHorarioEntrada.Items.Add("15:00");
            //    ddlHorarioEntrada.Items.Add("16:00");
            //    ddlHorarioEntrada.Items.Add("17:00");
            //    Cosonio++;
            //}

        }

        protected void txtFechaEntrada_TextChanged(object sender, EventArgs e)
        {
            if (DateTime.TryParse(txtFechaEntrada.Text, out DateTime fecha))
            {
                CargarTablaHorarios(fecha);
                lblMensaje.Text = "";
            }
            else
            {
                lblMensaje.Text = "Fecha inválida.";
                phTablaHorarios.Controls.Clear();
            }
        }

        private void CargarTablaHorarios(DateTime fecha)
        {
            List<Cita> citas = ObtenerCitasPorFecha(fecha);
            //lblMensaje.Text = ""+citas.;
            Table tabla = new Table();
            tabla.BorderWidth = 1;
            tabla.CellPadding = 2;
            tabla.CellSpacing = 0;
            tabla.Style["border-collapse"] = "collapse";
            tabla.Width = Unit.Pixel(400);

            TableHeaderRow header = new TableHeaderRow();

            TableHeaderCell thHora = new TableHeaderCell();
            thHora.Text = "Hora";
            thHora.Width = Unit.Pixel(90);
            thHora.Style["background-color"] = "#dddddd";
            thHora.Style["border"] = "1px solid black";
            thHora.Style["text-align"] = "center";
            header.Cells.Add(thHora);

            TableHeaderCell thArea1 = new TableHeaderCell();
            thArea1.Text = "Área 1";
            thArea1.Style["background-color"] = "#dddddd";
            thArea1.Style["border"] = "1px solid black";
            header.Cells.Add(thArea1);

            TableHeaderCell thArea2 = new TableHeaderCell();
            thArea2.Text = "Área 2";
            thArea2.Style["background-color"] = "#dddddd";
            thArea2.Style["border"] = "1px solid black";
            header.Cells.Add(thArea2);

            tabla.Rows.Add(header);

            DateTime horaBase = fecha.Date.AddHours(8);

            for (int i = 0; i < 11; i++)
            {
                TableRow fila = new TableRow();
                //fila.Height = Unit.Pixel(10);

                TableCell celHora = new TableCell();
                DateTime horaActual = horaBase.AddHours(i);
                celHora.Text = horaActual.ToString("hh:mm tt");
                celHora.Width = Unit.Pixel(80);
                celHora.Style["border"] = "1px solid black";
                celHora.Style["text-align"] = "center";
                fila.Cells.Add(celHora);

                TableCell celArea1 = new TableCell();
                celArea1.Style["border"] = "1px solid black";
                string textoArea1 = ObtenerModeloEnHora(citas, horaActual.TimeOfDay, "Área 1");
                if(textoArea1 != "")
                    celArea1.Text = NombreCli + ": " + textoArea1;
                //switch(TipoSer)
                //{
                //    //case -1:
                //    //    lblCorreo.Text = "aaa";
                //    //    break;
                //    case 1:
                //        celArea1.BackColor = System.Drawing.Color.Yellow;
                //        //lblCorreo.Text = "aaa";
                //        break;
                //    case 2:
                //        celArea1.BackColor = System.Drawing.Color.Green;

                //        break;
                //    case 3:
                //        celArea1.BackColor = System.Drawing.Color.Red;
                //        break;
                //}

                if(TipoSer == "0")
                {
                    celArea1.BackColor = System.Drawing.Color.Yellow;
                    //string script = @"Cosa"+ TipoSer;
                    //ClientScript.RegisterStartupScript(this.GetType(), "alertRedirect", script, true);
                }
                else if (TipoSer == "1")
                {
                    celArea1.BackColor = System.Drawing.Color.Green; 
                }
                else if (TipoSer == "2")
                {
                    celArea1.BackColor = System.Drawing.Color.Red;
                }
                TipoSer = "";
                NombreCli = "";

                fila.Cells.Add(celArea1);

                TableCell celArea2 = new TableCell();
                celArea2.Style["border"] = "1px solid black";
                string textoArea2 = ObtenerModeloEnHora(citas, horaActual.TimeOfDay, "Área 2");
                if (textoArea2 != "")
                    celArea2.Text = NombreCli +": " +textoArea2;
                if (TipoSer == "0")
                {
                    celArea2.BackColor = System.Drawing.Color.Yellow;
                    //string script = @"Cosa"+ TipoSer;
                    //ClientScript.RegisterStartupScript(this.GetType(), "alertRedirect", script, true);
                }
                else if (TipoSer == "1")
                {
                    celArea2.BackColor = System.Drawing.Color.Green;
                }
                else if (TipoSer == "2")
                {
                    celArea2.BackColor = System.Drawing.Color.Red;
                }
                //switch (TipoSer)
                //{
                //    case 1:
                //        celArea2.ForeColor = System.Drawing.Color.Yellow;
                //        break;
                //    case 2:
                //        celArea2.ForeColor = System.Drawing.Color.Green;

                //        break;
                //    case 3:
                //        celArea2.ForeColor = System.Drawing.Color.Red;
                //        break;
                //}
                fila.Cells.Add(celArea2);
                TipoSer = "";
                NombreCli = "";

                tabla.Rows.Add(fila);
            }

            phTablaHorarios.Controls.Clear();
            phTablaHorarios.Controls.Add(tabla);
        }

        private string ObtenerModeloEnHora(List<Cita> citas, TimeSpan hora, string area)
        {
            foreach (var cita in citas)
            {
                if (cita.Area == area && hora >= cita.HorarioEntrada && hora <= cita.HorarioSalida)
                {
                    TipoSer = cita.TipoServicio;
                    NombreCli = cita.NombreCliente;
                    return cita.ModeloCarro;
                }
            }
            return "";
        }

        private List<Cita> ObtenerCitasPorFecha(DateTime fecha)
        {
            List<Cita> lista = new List<Cita>();
            string dbFile = Server.MapPath("~/App_Data/CitasCarros.db");

            using (var con = new SQLiteConnection($"Data Source={dbFile};Version=3;"))
            {
                con.Open();

                string sql = @"
                    SELECT Id, NombreCliente, CorreoCliente, ModeloCarro, FechaEntrada, HorarioEntrada, HorarioSalida, TipoServicio, Area 
                    FROM Citas
                    WHERE FechaEntrada = @fecha;";

                using (var cmd = new SQLiteCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@fecha", fecha.ToString("yyyy-MM-dd"));

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var cita = new Cita()
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                NombreCliente = reader["NombreCliente"].ToString(),
                                CorreoCliente = reader["CorreoCliente"].ToString(),
                                ModeloCarro = reader["ModeloCarro"].ToString(),
                                FechaEntrada = DateTime.Parse(reader["FechaEntrada"].ToString()),
                                HorarioEntrada = TimeSpan.Parse(reader["HorarioEntrada"].ToString()),
                                HorarioSalida = TimeSpan.Parse(reader["HorarioSalida"].ToString()),
                                TipoServicio = reader["TipoServicio"].ToString(),
                                Area = reader["Area"].ToString()
                            };
                            lista.Add(cita);
                        }
                    }
                }
            }

            return lista;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string dbFile = Server.MapPath("~/App_Data/CitasCarros.db");

            try
            {
                using (var con = new SQLiteConnection($"Data Source={dbFile};Version=3;"))
                {
                    con.Open();


                    string sqlCreateTable = @"
                CREATE TABLE IF NOT EXISTS Citas (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    NombreCliente TEXT NOT NULL,
                    CorreoCliente TEXT NOT NULL,
                    ModeloCarro TEXT NOT NULL,
                    FechaEntrada TEXT NOT NULL,
                    HorarioEntrada TEXT NOT NULL,
                    HorarioSalida TEXT NOT NULL,
                    TipoServicio TEXT NOT NULL,
                    Area TEXT NOT NULL
                );";
                    using (var cmdCreate = new SQLiteCommand(sqlCreateTable, con))
                    {
                        cmdCreate.ExecuteNonQuery();
                    }


                    TimeSpan horaNuevaEntrada = new TimeSpan(HoraEntrada, 0, 0);
                    TimeSpan horaNuevaSalida = new TimeSpan(HoraSalida, 0, 0);


                    string sqlSelect = @"
                        SELECT HorarioEntrada, HorarioSalida
                        FROM Citas
                        WHERE FechaEntrada = @fecha AND Area = @area";

                    using (var cmdSelect = new SQLiteCommand(sqlSelect, con))
                    {
                        cmdSelect.Parameters.AddWithValue("@fecha", txtFechaEntrada.Text.Trim());
                        cmdSelect.Parameters.AddWithValue("@area", ddlArea.SelectedValue);

                        using (var reader = cmdSelect.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TimeSpan horaExistenteEntrada = TimeSpan.Parse(reader["HorarioEntrada"].ToString());
                                TimeSpan horaExistenteSalida = TimeSpan.Parse(reader["HorarioSalida"].ToString());


                                TimeSpan HoraLimite = TimeSpan.Parse("20:00");
                                bool traslape =
                                    horaNuevaEntrada <= horaExistenteSalida &&
                                    horaNuevaSalida >= horaExistenteEntrada && HoraSalida < 19;

                                if (traslape)
                                {
                                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                                    lblMensaje.Text = "Ya existe una cita en ese horario para esa área.";
                                    if (DateTime.TryParse(txtFechaEntrada.Text, out DateTime fecha2))
                                    {
                                        CargarTablaHorarios(fecha2);
                                    }
                                    return;
                                }
                            }
                        }
                    }

                    string sqlInsert = @"
                INSERT INTO Citas 
                (NombreCliente, CorreoCliente, ModeloCarro, FechaEntrada, HorarioEntrada, HorarioSalida, TipoServicio, Area)
                VALUES (@nombre, @correo, @modelo, @fecha, @horaEntrada, @horaSalida, @servicio, @area);";

                    using (var cmdInsert = new SQLiteCommand(sqlInsert, con))
                    {
                        cmdInsert.Parameters.AddWithValue("@nombre", txtNombre.Text.Trim());
                        cmdInsert.Parameters.AddWithValue("@correo", txtCorreo.Text.Trim());
                        cmdInsert.Parameters.AddWithValue("@modelo", txtModelo.Text.Trim());
                        cmdInsert.Parameters.AddWithValue("@fecha", txtFechaEntrada.Text.Trim());
                        cmdInsert.Parameters.AddWithValue("@horaEntrada", horaNuevaEntrada.ToString(@"hh\:mm"));
                        if (ddlHorarioEntrada.SelectedIndex == 0)
                        {
                            if (ddTipoSer.SelectedIndex == 0)
                            {
                                cmdInsert.Parameters.AddWithValue("@horaSalida", "9:00");
                                //lblArea.Text = "1" + HoraSalida;
                            }
                            else if (ddTipoSer.SelectedIndex == 1)
                            {
                                cmdInsert.Parameters.AddWithValue("@horaSalida", "10:00");
                                //lblArea.Text = "2" + HoraSalida;
                            }
                            else if (ddTipoSer.SelectedIndex == 2)
                            {
                                cmdInsert.Parameters.AddWithValue("@horaSalida", "11:00");
                                //lblArea.Text = "3" + HoraSalida;
                            }
                        }
                        else
                            cmdInsert.Parameters.AddWithValue("@horaSalida", horaNuevaSalida.ToString(@"hh\:mm"));
                        //lblArea.Text = ""+ horaNuevaSalida.ToString(@"hh\:mm");
                        //lblArea.Text = "" + HoraSalida;
                        cmdInsert.Parameters.AddWithValue("@servicio", ddTipoSer.Text);
                        cmdInsert.Parameters.AddWithValue("@area", ddlArea.SelectedValue);

                        cmdInsert.ExecuteNonQuery();
                    }

                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                    lblMensaje.Text = "Cita guardada correctamente.";
                    
                    txtNombre.Text = "";
                    txtModelo.Text = "";
                    ddlHorarioEntrada.Items.Clear();
                    ddlHorarioEntrada.Items.Add("08:00");
                    ddlHorarioEntrada.Items.Add("09:00");
                    ddlHorarioEntrada.Items.Add("10:00");
                    ddlHorarioEntrada.Items.Add("11:00");
                    ddlHorarioEntrada.Items.Add("12:00");
                    ddlHorarioEntrada.Items.Add("13:00");
                    ddlHorarioEntrada.Items.Add("14:00");
                    ddlHorarioEntrada.Items.Add("15:00");
                    ddlHorarioEntrada.Items.Add("16:00");
                    ddlHorarioEntrada.Items.Add("17:00");
                    //ddlHorarioEntrada.Items.Add("18:00");

                    ddlHorarioEntrada.SelectedIndex = 0;
                    HoraEntrada = 8;
                    HoraSalida = 9;
                    ddTipoSer.SelectedIndex = 0;
                    ddlArea.SelectedIndex = 0;
                    

                    if (DateTime.TryParse(txtFechaEntrada.Text, out DateTime fecha))
                    {
                        CargarTablaHorarios(fecha);
                    }
                    Correo();
                    txtCorreo.Text = "";
                }
            }
            catch (ArgumentException ex) when (ex.ParamName == "addresses")
            {
                lblMensaje.ForeColor = System.Drawing.Color.Black;
                lblMensaje.Text = "Error al enviar correo, correo inválido.";
            }
            catch (Exception ex)
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Error al guardar: " + ex.Message;
            }
        }

        protected void ddHorarioEnt_Cambio(object sender, EventArgs e)
        {
            switch(ddlHorarioEntrada.SelectedIndex)
            {
                case 0:
                    HoraEntrada = 8;
                    break;
                case 1:
                    HoraEntrada = 9;
                    break;
                case 2:
                    HoraEntrada = 10;
                    break;
                case 3:
                    HoraEntrada = 11;
                    break;
                case 4:
                    HoraEntrada = 12;
                    break;
                case 5:
                    HoraEntrada = 13;
                    break;
                case 6:
                    HoraEntrada = 14;
                    break;
                case 7:
                    HoraEntrada = 15;
                    break;
                case 8:
                    HoraEntrada = 16;
                    break;
                case 9:
                    HoraEntrada = 17;
                    break;
                case 10:
                    HoraEntrada = 18;
                    break;
                case 11:
                    HoraEntrada = 19;
                    break;
            }
            switch (ddTipoSer.SelectedIndex)
            {
                case 0:
                    HoraSalida = HoraEntrada + 1;
                    //ddlHorarioEntrada.Items.Clear();
                    //ddlHorarioEntrada.Items.Add("08:00");
                    //ddlHorarioEntrada.Items.Add("09:00");
                    //ddlHorarioEntrada.Items.Add("10:00");
                    //ddlHorarioEntrada.Items.Add("11:00");
                    //ddlHorarioEntrada.Items.Add("12:00");
                    //ddlHorarioEntrada.Items.Add("13:00");
                    //ddlHorarioEntrada.Items.Add("14:00");
                    //ddlHorarioEntrada.Items.Add("15:00");
                    //ddlHorarioEntrada.Items.Add("16:00");
                    //ddlHorarioEntrada.Items.Add("17:00");
                    break;
                case 1:
                    HoraSalida = HoraEntrada + 2;
                    //ddlHorarioEntrada.Items.Clear();
                    //ddlHorarioEntrada.Items.Add("08:00");
                    //ddlHorarioEntrada.Items.Add("09:00");
                    //ddlHorarioEntrada.Items.Add("10:00");
                    //ddlHorarioEntrada.Items.Add("11:00");
                    //ddlHorarioEntrada.Items.Add("12:00");
                    //ddlHorarioEntrada.Items.Add("13:00");
                    //ddlHorarioEntrada.Items.Add("14:00");
                    //ddlHorarioEntrada.Items.Add("15:00");
                    //ddlHorarioEntrada.Items.Add("16:00");
                    break;
                case 2:
                    HoraSalida = HoraEntrada + 3;
                    //ddlHorarioEntrada.Items.Clear();
                    //ddlHorarioEntrada.Items.Add("08:00");
                    //ddlHorarioEntrada.Items.Add("09:00");
                    //ddlHorarioEntrada.Items.Add("10:00");
                    //ddlHorarioEntrada.Items.Add("11:00");
                    //ddlHorarioEntrada.Items.Add("12:00");
                    //ddlHorarioEntrada.Items.Add("13:00");
                    //ddlHorarioEntrada.Items.Add("14:00");
                    //ddlHorarioEntrada.Items.Add("15:00");
                    break;
            }
            //if (DateTime.TryParse(txtFechaEntrada.Text, out DateTime fecha))
            //{
            //    CargarTablaHorarios(fecha);
            //}
        }

        protected void ddTipoSer_Cambio(object sender, EventArgs e)
        {
            switch (ddlHorarioEntrada.SelectedIndex)
            {
                case 0: HoraEntrada = 8; break;
                case 1: HoraEntrada = 9; break;
                case 2: HoraEntrada = 10; break;
                case 3: HoraEntrada = 11; break;
                case 4: HoraEntrada = 12; break;
                case 5: HoraEntrada = 13; break;
                case 6: HoraEntrada = 14; break;
                case 7: HoraEntrada = 15; break;
                case 8: HoraEntrada = 16; break;
                case 9: HoraEntrada = 17; break;
                case 10: HoraEntrada = 18; break;
                case 11: HoraEntrada = 19; break;
            }

            switch (ddTipoSer.SelectedIndex)
            {
                case 0: HoraSalida = HoraEntrada + 1; break;
                case 1: HoraSalida = HoraEntrada + 2; break;
                case 2: HoraSalida = HoraEntrada + 3; break;
            }

            ddlHorarioEntrada.Items.Clear();
            int maxHora = 19 - (ddTipoSer.SelectedIndex + 2);
            for (int hora = 8; hora <= maxHora; hora++)
            {
                ddlHorarioEntrada.Items.Add($"{hora:00}:00");
            }
            txtFechaEntrada.Text = DateTime.Today.ToString("yyyy-MM-dd");
            CargarTablaHorarios(DateTime.Today);

        }

        protected void btnReiniciar_Click(object sender, EventArgs e)
        {
            string dbFile = Server.MapPath("~/App_Data/CitasCarros.db");

            try
            {
                using (var con = new SQLiteConnection($"Data Source={dbFile};Version=3;"))
                {
                    con.Open();

                    

                    string sqlDelete = @"
                        Delete From Citas;";

                    using (var cmdInsert = new SQLiteCommand(sqlDelete, con))
                    {
                        //cmdInsert.Parameters.AddWithValue("@nombre", txtNombre.Text.Trim());
                        //cmdInsert.Parameters.AddWithValue("@correo", txtCorreo.Text.Trim());
                        //cmdInsert.Parameters.AddWithValue("@modelo", txtModelo.Text.Trim());
                        //cmdInsert.Parameters.AddWithValue("@fecha", txtFechaEntrada.Text.Trim());
                        //cmdInsert.Parameters.AddWithValue("@horaEntrada", ddlHorarioEntrada.Text.Trim());
                        //cmdInsert.Parameters.AddWithValue("@horaSalida", txtHorarioSalida.Text.Trim());
                        //cmdInsert.Parameters.AddWithValue("@servicio", ddTipoSer.Text.Trim());
                        //cmdInsert.Parameters.AddWithValue("@area", ddlArea.SelectedValue);

                        cmdInsert.ExecuteNonQuery();
                    }
                }

                lblMensaje.ForeColor = System.Drawing.Color.Green;
                //lblMensaje.Text = "Cita guardada correctamente.";


                txtNombre.Text = "";
                txtCorreo.Text = "";
                txtModelo.Text = "";


                ddlHorarioEntrada.SelectedIndex = 0;
                //txtHorarioSalida.Text = "";
                HoraEntrada = 8;
                HoraSalida = 9;
                ddTipoSer.SelectedIndex = 0;
                ddlArea.SelectedIndex = 0;


                if (DateTime.TryParse(txtFechaEntrada.Text, out DateTime fecha))
                {
                    CargarTablaHorarios(fecha);
                }
            }
            catch (Exception ex)
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Error al guardar: " + ex.Message;
            }
        }
        public void Correo()
        {
            MailMessage correo = new MailMessage();
            correo.From = new MailAddress("Empresonio@gmail.com", "Empresonio");
            correo.To.Add(txtCorreo.Text);
            correo.Subject = "Cita agendada";
            correo.Body = "Cita agendada para la fecha: " + txtFechaEntrada.Text + " y hora de entrada a: " + ddlHorarioEntrada.Text + " con hora de salida de: " + (HoraSalida + 1) + ":00";
            correo.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("Empresonio@gmail.com", "dsme pofa giey arya");
            smtp.EnableSsl = true;
            smtp.Send(correo);

        }
    }
}