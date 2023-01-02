using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibreriaClases;

namespace LibreriaExperto.Seguridad
{
    public static class ValidacionParametros
    {
        public static ErrorPropy ValidacionBusqueda(decimal precioDesde, decimal precioHasta) {
            ErrorPropy error = new ErrorPropy();
            if (precioDesde>precioHasta) {
                error.codigoError = -1;
                error.descripcionError = "El precio desde no puede ser mayor al precio hasta.";
                return error;
            }
            return error;
        }
        public static ErrorPropy ValidacionEditarPublicacion(string tipoPropiedad, string tipoPublicacion, string tipoConstruccion, string tipoMoneda, int superficieTerreno, int superficieCubierta, decimal precioPropiedad, int añosAntiguedad, int nroPisos, List<string> imagenes) {
            ErrorPropy error = new ErrorPropy();
            
            
            if (String.IsNullOrEmpty(tipoPropiedad))
            {
                error.codigoError = -1;
                error.descripcionError = "El campo Tipo de Propiedad no puede estar vacío";
                return error;
            }

            if (String.IsNullOrEmpty(tipoPublicacion))
            {
                error.codigoError = -1;
                error.descripcionError = "El campo Tipo de Publicacion no puede estar vacío";
                return error;
            }
            //if (String.IsNullOrEmpty(tipoPublicante))
            //{
            //    error.codigoError = -1;
            //    error.descripcionError = "El campo Tipo de Publicante no puede estar vacío";
            //    return error;
            //}
            if (String.IsNullOrEmpty(tipoConstruccion))
            {
                error.codigoError = -1;
                error.descripcionError = "El campo Tipo de Construccion no puede estar vacío";
                return error;
            }
            if (String.IsNullOrEmpty(tipoMoneda))
            {
                error.codigoError = -1;
                error.descripcionError = "El campo Tipo de Moneda no puede estar vacío";
                return error;
            }
            if (superficieTerreno == 0)
            {
                error.codigoError = -1;
                error.descripcionError = "La superficie del terreno no puede ser 0 (cero)";
                return error;
            }
            if (superficieTerreno < 0)
            {
                error.codigoError = -1;
                error.descripcionError = "La superficie del terreno debe ser mayor o igual a 0 (cero)";
                return error;
            }
            if (superficieCubierta == 0)
            {
                error.codigoError = -1;
                error.descripcionError = "La superficie cubierta no puede ser 0";
                return error;
            }
            if (superficieCubierta < 0)
            {
                error.codigoError = -1;
                error.descripcionError = "La superficie cubierta debe ser mayor o igual a 0";
                return error;
            }
            if (superficieTerreno < superficieCubierta)
            {
                error.codigoError = -1;
                error.descripcionError = "La superficie cubierta no puede ser mayor a la superficie del terreno";
                return error;
            }
            if (nroPisos < 0)
            {
                error.codigoError = -1;
                error.descripcionError = "El número de pisos debe ser mayor o igual a 0 (ceros)";
                return error;
            }
            if (añosAntiguedad < 0)
            {
                error.codigoError = -1;
                error.descripcionError = "Los años de antiguedad deben ser mayores o iguales a 0 (cero)";
                return error;
            }
            if (precioPropiedad == 0 || precioPropiedad < 0)
            {
                error.codigoError = -1;
                error.descripcionError = "Debe ingresar un valor mayor o igual a cero para el Precio de la propiedad";
                return error;
            }
            
            return error;
        }
        public static ErrorPropy ValidacionCrearPublicacion(string ubicacion,string tipoPropiedad,string tipoPublicacion,string tipoPublicante,string tipoConstruccion,string tipoMoneda,int superficieTerreno,int superficieCubierta,decimal precioPropiedad,int añosAntiguedad, int nroPisos, List<string> imagenes) {
            ErrorPropy error = new ErrorPropy();
            if (String.IsNullOrEmpty(ubicacion)) {
                error.codigoError = -1;
                error.descripcionError = "El campo Ubicación no puede estar vacío";
                return error;
            }
            if (ubicacion.GetType()!=typeof(String)) {
                error.codigoError = -1;
                error.descripcionError = "Formato no válido para el parámetro Ubicación";
                return error;
            }
            if (String.IsNullOrEmpty(tipoPropiedad)) {
                error.codigoError = -1;
                error.descripcionError = "El campo Tipo de Propiedad no puede estar vacío";
                return error;
            }
            
            if (String.IsNullOrEmpty(tipoPublicacion)) {
                error.codigoError = -1;
                error.descripcionError = "El campo Tipo de Publicacion no puede estar vacío";
                return error;
            }
            //if (String.IsNullOrEmpty(tipoPublicante)) {
            //    error.codigoError = -1;
            //    error.descripcionError = "El campo Tipo de Publicante no puede estar vacío";
            //    return error;
            //}
            //if (String.IsNullOrEmpty(tipoConstruccion)) {
            //    error.codigoError = -1;
            //    error.descripcionError = "El campo Tipo de Construccion no puede estar vacío";
            //    return error;
            //}
            if (String.IsNullOrEmpty(tipoMoneda)) {
                error.codigoError = -1;
                error.descripcionError = "El campo Tipo de Moneda no puede estar vacío";
                return error;
            }
            if (superficieTerreno==0) {
                error.codigoError = -1;
                error.descripcionError = "La superficie del terreno no puede ser 0 (cero)";
                return error;
            }
            if (superficieTerreno<0) {
                error.codigoError = -1;
                error.descripcionError = "La superficie del terreno debe ser mayor o igual a 0 (cero)";
                return error;
            }
            if (superficieCubierta==0) {
                error.codigoError = -1;
                error.descripcionError = "La superficie cubierta no puede ser 0";
                return error;
            }
            if (superficieCubierta<0) {
                error.codigoError = -1;
                error.descripcionError = "La superficie cubierta debe ser mayor o igual a 0";
                return error;
            }
            if (superficieTerreno<superficieCubierta) {
                error.codigoError = -1;
                error.descripcionError = "La superficie cubierta no puede ser mayor a la superficie del terreno";
                return error;
            }
            if (nroPisos<0) {
                error.codigoError = -1;
                error.descripcionError = "El número de pisos debe ser mayor o igual a 0 (ceros)";
                return error;
            }
            if (añosAntiguedad<0) {
                error.codigoError = -1;
                error.descripcionError = "Los años de antiguedad deben ser mayores o iguales a 0 (cero)";
                return error;
            }
            if (precioPropiedad==0 || precioPropiedad<0) {
                error.codigoError = -1;
                error.descripcionError = "Debe ingresar un valor mayor o igual a cero para el Precio de la propiedad";
                return error;
            }
            if (imagenes.Count<=0) {
                error.codigoError = -1;
                error.descripcionError = "Debe agregar al menos una imagen de su inmueble";
                return error;
            }
            //foreach (var imagen in imagenes)
            //{
            //    if (!imagen.Contains("image/png") || !imagen.Contains("image/jpeg") || !imagen.Contains("image/jpg"))
            //    {
            //        error.codigoError = -1;
            //        error.descripcionError = "La imagen no tiene el formato indicado. Asegurese que la imagen es .png, .jpeg o .jpg";
            //        return error;
            //    }
                
            //}
            return error;
        }
        public static ErrorPropy ValidacionRecuperarContraseñaPaso1(string email) {
            ErrorPropy error = new ErrorPropy();
            if (String.IsNullOrEmpty(email))
            {
                error.codigoError = -1;
                error.descripcionError = "El campo email no puede estar vacío";
                return error;
            }
            if (!email.Contains("@")) {
                error.codigoError = -1;
                error.descripcionError = "Debe ingresar una dirección de correo válida";
                return error;
            }
            
            return error;

        }
        public static ErrorPropy ValidacionRecuperarContraseñaPaso2(string email,string clave,string claveRepetida) {
            ErrorPropy error = new ErrorPropy();
            if (String.IsNullOrEmpty(email)) {
                error.codigoError = -1;
                error.descripcionError = "El campo email no puede estar vacío";
                return error;
            }
            if (!email.Contains("@"))
            {
                error.codigoError = -1;
                error.descripcionError = "Debe ingresar una dirección de correo válida";
                return error;
            }
            if (String.IsNullOrEmpty(clave)) {
                error.codigoError = -1;
                error.descripcionError = "El campo clave no puede estar vacío";
                return error;
            }
            if (String.IsNullOrEmpty(claveRepetida)) {
                error.codigoError = -1;
                error.descripcionError = "El campo clave repetida no puede estar vacío";
                return error;
            }
            if (clave!=claveRepetida) {
                error.codigoError = -1;
                error.descripcionError = "Las claves no coinciden";
                return error;
            }
            error = ValidacionComposicionClave(clave);
            return error;
        }
        public static ErrorPropy ValidacionLogin(string email,string clave) {
            ErrorPropy error = new ErrorPropy();
            if (String.IsNullOrEmpty(email)) {
                error.codigoError = -1;
                error.descripcionError = "El campo email no puede estar vacío.";
                return error;
            }
            if (String.IsNullOrEmpty(clave)) {
                error.codigoError = -1;
                error.descripcionError = "El campo clave no puede estar vacío";
                return error;
            }
            if (!email.Contains("@")) {
                error.codigoError = -1;
                error.descripcionError = "Debe ingresar una dirección de correo electrónico válida";
                return error;
            }
            return error;
        }
        public static ErrorPropy ValidacionParametrosCrearCuenta(string nombreUsuario,string apellidoUsuario,string telefono1,string telefono2,string email, string clave, string claveRepetida) {
            ErrorPropy error = new ErrorPropy();
            if (String.IsNullOrEmpty(nombreUsuario))
            {
                error.codigoError = -1;
                error.descripcionError = "El campo Nombre no puede estar vacío";
                return error;
            }
            if (String.IsNullOrEmpty(apellidoUsuario)) {
                error.codigoError = -1;
                error.descripcionError = "El campo Apellido no puede estar vacío";
                return error;
            }
            if (String.IsNullOrEmpty(telefono1)) {
                error.codigoError = -1;
                error.descripcionError = "Debe ingresar un número de teléfono";
                return error;
            }
            if (!telefono1.All(char.IsDigit)) {
                error.codigoError = -1;
                error.descripcionError = "El formato para teléfono 1 ingresado no es correcto, ingrese sólo números";
                return error;
            }
            if (!String.IsNullOrEmpty(telefono2) && !telefono2.All(char.IsDigit)) {
                error.codigoError = -1;
                error.descripcionError = "El formato para teléfono 2 ingresado no es correcto, ingrese sólo números";
                return error;
            }
            if (String.IsNullOrEmpty(email)) {
                error.codigoError = -1;
                error.descripcionError = "El campo Email no puede estar vacío";
                return error;
            }
            if (!email.Contains("@")) {
                error.codigoError = -1;
                error.descripcionError = "Debe ingresar un email válido";
            }
            if (String.IsNullOrEmpty(clave)) {
                error.codigoError = -1;
                error.descripcionError = "El campo Clave no puede estar vacío";
                return error;
            }
            if (String.IsNullOrEmpty(claveRepetida)) {
                error.codigoError = -1;
                error.descripcionError = "El campo Clave repetida no puede estar vacío";
                return error;
            }
            if (clave!=claveRepetida) {
                error.codigoError = -1;
                error.descripcionError = "Las claves ingresadas no coinciden";
                return error;
            }
            error = ValidacionComposicionClave(clave);
            return error;
        }
        public static ErrorPropy ValidacionComposicionClave(string clave)
        {
            //La composición de la clave debe ser la siguiente:
            //Debe tener al menos 1 número. Debe tener al menos una letra mayúscula. Debe tener al menos un caracter especial. La longitud debe ser de al menos 6 caracteres.
            ErrorPropy error = new ErrorPropy();
            if (!clave.Any(char.IsDigit))
            {
                error.codigoError = -1;
                error.descripcionError = "La clave debe tener al menos 1 (uno) número.";
                return error;
            }
            if (!clave.Any(c => char.IsUpper(c)))
            {
                error.codigoError = -1;
                error.descripcionError = "La clave debe tener al menos una letra mayúscula.";
                return error;
            }
            if (clave.All(c => char.IsLetterOrDigit(c)))
            {
                error.codigoError = -1;
                error.descripcionError = "La clave debe tener al menos un caracter especial (@/_*+-)";
            }
            if (clave.Length < 6)
            {
                error.codigoError = 1;
                error.descripcionError = "La clave debe tener al menos 6 caracteres.";
                return error;
            }
            return error;

        }
    }
}
