using Azure.Storage.Blobs;
using MvcCubos.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MvcCubos.Services
{
    public class ServiceApiCubos
    {
        private BlobServiceClient client;

        private MediaTypeWithQualityHeaderValue Header;
        private string UrlApiUsuarios;


        public ServiceApiCubos(IConfiguration configuration, BlobServiceClient client)
        {
            this.UrlApiUsuarios =
                configuration.GetValue<string>("ApiUrls:ApiCubos");
            this.Header =
                new MediaTypeWithQualityHeaderValue("application/json");

            this.client = client;
        }

        public async Task<string> GetTokenAsync(string username, string password)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/auth/login";
                client.BaseAddress = new Uri(this.UrlApiUsuarios);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                LoginModel model = new LoginModel
                {
                    UserName = username,
                    Password = password
                };

                string jsonModel = JsonConvert.SerializeObject(model);
                StringContent content =
                    new StringContent(jsonModel, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);
                if (response.IsSuccessStatusCode)
                {
                    string data =
                        await response.Content.ReadAsStringAsync();
                    JObject jsonObject = JObject.Parse(data);
                    string token =
                        jsonObject.GetValue("response").ToString();
                    return token;
                }
                else
                {
                    return null;
                }
            }
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApiUsuarios);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data =
                        await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }

            }
        }

        private async Task<T> CallApiAsync<T>(string request, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApiUsuarios);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add
                    ("Authorization", "bearer " + token);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data =
                        await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task GetRegisterUserAsync
           (string nombre, string email,string password, string imagen)
        {

            using (HttpClient client = new HttpClient())
            {
                string request = "/api/auth/register";
                client.BaseAddress = new Uri(this.UrlApiUsuarios);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                UsuariosCubo usuario = new UsuariosCubo();
                usuario.IdUsuario = 0;
                usuario.Nombre = nombre;
                usuario.Email = email;
                usuario.Password = password;
                usuario.Imagen = imagen;

                string json = JsonConvert.SerializeObject(usuario);

                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(request, content);
            }
        }


        //METODO PROTEGIDO PARA RECUPERAR EL PERFIL
        public async Task<UsuariosCubo> GetPerfilUsuarioAsync
            (string token)
        {
            string request = "/api/usuarios/perfilusuario";
            UsuariosCubo usuario = await
                this.CallApiAsync<UsuariosCubo>(request, token);
            return usuario;
        }

        public async Task<List<Cubos>> GetCubosAsync()
        {
            string request = "/api/cubos/productos";
            List<Cubos> cubos = await
                this.CallApiAsync<List<Cubos>>(request);
            return cubos;
        }

        public async Task<Cubos> FindCubosAsync(int idcubo)
        {
            string request = "/api/cubos/producto/" + idcubo;
            return await this.CallApiAsync<Cubos>(request);
        }

        public async Task<List<string>> GetMarcas()
        {
            string request = "/api/cubos/Marca";
            List<string> marcas = await
                this.CallApiAsync<List<string>>(request);
            return marcas;
        }

        public async Task<List<Cubos>> GetMarcasList(string marca)
        {
            string request = "/api/cubos/Marca/" + marca;
            List<Cubos> marcas = await
                this.CallApiAsync<List<Cubos>>(request);
            return marcas;
        }


        public async Task InsertCuboAsync
          (int idcubo, string nombre, string marca,string imagen ,int precio)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/Cubos";
                client.BaseAddress = new Uri(this.UrlApiUsuarios);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                Cubos pj = new Cubos();
                pj.IdCubo = idcubo;
                pj.Nombre = nombre;
                pj.Marca = marca;
                pj.Imagen = imagen;
                pj.Precio = precio;

                string json = JsonConvert.SerializeObject(pj);

                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(request, content);

            }
        }



    }
}
