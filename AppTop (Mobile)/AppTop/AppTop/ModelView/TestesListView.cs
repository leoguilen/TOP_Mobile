using AppTop.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace AppTop.ModelView
{
    public class TestesListView
    {
        private Resultado _oldResultado;
        public static string Username;
        public ObservableCollection<Resultado> resultados { get; set; }
        
        public TestesListView()
        {
            resultados = new ObservableCollection<Resultado>();
            //IEnumerable<Resultado> results = HttpClientResultado.GetResult(Username);

            foreach (var itemResults in HttpClientResultado.GetResult(Username).OrderByDescending(r=>r.DataFim))
            {
                resultados.Add(new Resultado
                {
                    DataFim = itemResults.DataFim,
                    DataInicio = itemResults.DataInicio,
                    DescArea = itemResults.DescArea.ToUpper(),
                    DuracaoCurso = itemResults.DuracaoCurso,
                    EstadoFaculdade = itemResults.EstadoFaculdade,
                    IdTeste = itemResults.IdTeste,
                    IdUsuario = itemResults.IdUsuario,
                    ImagemCurso = itemResults.ImagemCurso,
                    NomeCurso = itemResults.NomeCurso.ToUpper(),
                    NomeFaculdade = itemResults.NomeFaculdade,
                    NotaMEC = itemResults.NotaMEC,
                    ResultadoBiologicas = itemResults.ResultadoBiologicas,
                    ResultadoExatas = itemResults.ResultadoExatas,
                    ResultadoHumanas = itemResults.ResultadoHumanas,
                    SiteFaculdade = itemResults.SiteFaculdade,
                    TempoConclusao = itemResults.TempoConclusao,
                    TipoCurso = itemResults.TipoCurso,
                    Username = itemResults.Username,
                    Isvisible = false,
                    IsvisibleDown = true,
                    IsvisibleUp = false
                });
                
            }


        }
        
        public void ShowOrHiddenResults(Resultado result)
        {
            if(_oldResultado == result)
            {
                result.Isvisible = !result.Isvisible;
                result.IsvisibleDown = true;
                result.IsvisibleUp = false;
            } else
            {
                if(_oldResultado != null)
                {
                    _oldResultado.Isvisible = false;
                    result.IsvisibleDown = true;
                    result.IsvisibleUp = false;
                    UpdateResults(_oldResultado);
                }

                result.Isvisible = true;
                result.IsvisibleDown = false;
                result.IsvisibleUp = true;
                UpdateResults(result);
            }
            _oldResultado = result;

        }

        private void UpdateResults(Resultado res)
        {
            var index = resultados.IndexOf(res);
            resultados.Remove(res);
            resultados.Insert(index, res);
        }

    }
}
