namespace SistemaVenta.AplicacionWeb.Utilidades.Response
{
    public class GenericResponse<TObject>
    {
        public string? Mensaje { get; set; }
        public TObject? Objeto { get; set; }
        public List<TObject>? ListaObjeto { get; set; }
        public int? Confirmacion { get; set; }
    }
}
