﻿using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;

namespace CapaLogica
{
    public class logEmleados
    {
        private static readonly logEmleados _instancia = new logEmleados();
        public static logEmleados Instancia
        {
            get { return logEmleados._instancia; }
        }

        public void ActualizarEstadoEsPersonal(int idEmpleado)
        {
            try
            {
                // Llamar al método de la capa de datos para actualizar el estado
                datEmpleados.Instancia.ActualizarEstadoEsPersonal(idEmpleado);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones (registrar en log, notificar al usuario, etc.)
                throw new Exception("Error al actualizar el estado de esPersonalLimpieza en la capa lógica: " + ex.Message);
            }
        }


        // Método para insertar un empleado (usa datEmpleados)
        public void InsertarEmpleado(string nombres, string apellidos, bool esApoyo, string direccionCorreo, string urlFoto, string DNI, bool? esPersonalLimpieza)
        {
            try
            {
                datEmpleados.Instancia.InsertarEmpleado(nombres, apellidos, esApoyo, direccionCorreo, urlFoto, DNI, esPersonalLimpieza);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones (registrar en log, notificar al usuario, etc.)
                throw new Exception("Error al insertar el empleado: " + ex.Message);
            }
        }


        public DataTable ObtenerTodosLosEmpleados()
        {
            try
            {
                return datEmpleados.Instancia.ObtenerTodosLosEmpleados();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la capa de lógica al obtener los empleados: " + ex.Message);
            }
        }
        public int ObtenerEmpleadoIdPorNombre(string nombreCompleto)
        {
            try
            {
                return datEmpleados.Instancia.ObtenerEmpleadoIdPorNombre(nombreCompleto);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la capa de lógica al obtener el ID del empleado: " + ex.Message);
            }
        }

        public string ObtenerNombrePorID(int id)
        {
            try
            {
                return datEmpleados.Instancia.ObtenerNombreCompletoPorEmpleadoId(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la capa de lógica al obtener el ID del empleado: " + ex.Message);
            }
        }

        public DataTable ObtenerDetallesEmpleadoPorId(int empleadoId)
        {
            try
            {
                return datEmpleados.Instancia.ObtenerDetallesEmpleadoPorId(empleadoId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la capa de lógica al obtener detalles del empleado: " + ex.Message);
            }
        }
        public void ModificarEmpleado(int empleadoId, bool esApoyo, string direccionCorreo, string urlFoto, string DNI, DateTime fechaNacimiento)
        {
            try
            {
                datEmpleados.Instancia.ModificarEmpleado(empleadoId, esApoyo, direccionCorreo, urlFoto, DNI, fechaNacimiento);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la capa de lógica al modificar el empleado: " + ex.Message);
            }
        }
        public bool EliminarEmpleado(int empleadoId, out string mensajeError)
        {
            return datEmpleados.Instancia.EliminarEmpleado(empleadoId, out mensajeError);
        }
        public DataTable ObtenerEmpleadosFiltrados(string filtro)
        {
            try
            {
                return datEmpleados.Instancia.ObtenerEmpleadosFiltrados(filtro);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la capa de lógica al obtener empleados filtrados: " + ex.Message);
            }
        }
        public DataTable ObtenerEmpleadosPersonalYParques(string filtro)
        {
            try
            {
                return datEmpleados.Instancia.ObtenerEmpleadosPersonalYParques(filtro);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones (puedes registrar en un log, mostrar mensajes, etc.)
                throw new Exception("Error al obtener los empleados y parques: " + ex.Message);
            }
        }
        public DataTable ListarEmpleadosQueSeanLimpieza()
        {
            try
            {
                return datEmpleados.Instancia.ListarEmpleadosQueSeanLimpieza();
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception("Error al listar empleados que son personal de limpieza: " + ex.Message);
            }
        }

        public DataTable datObtenerPersonalLimpiezaParaComboBox()
        {
            try
            {
                return datEmpleados.Instancia.datObtenerPersonalLimpiezaParaComboBox();
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception("Error al listar empleados que son personal de limpieza: " + ex.Message);
            }
        }



    }
}
