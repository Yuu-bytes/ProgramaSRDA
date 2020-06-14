using Microsoft.CodeAnalysis.Differencing;
using Microsoft.VisualBasic;
using ProgramaSRDAMVC.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LiteDatabase.Services
{
    public class DataBase
    {
        public bool Inserir(EntradaDTO newEntrada)
        {
            using (var db = new LiteDB.LiteDatabase("BancoDeDados.db"))
            {
                newEntrada.Id = Guid.NewGuid();
                var entradaCollection = db.GetCollection<EntradaDTO>("entradas");
                entradaCollection.Insert(newEntrada);
                return true;
            }
        }

        public List<EntradaDTO> Listar()
        {
            using (var db = new LiteDB.LiteDatabase("BancoDeDados.db"))
            {
                var entradaCollection = db.GetCollection<EntradaDTO>("entradas");
                return entradaCollection.FindAll().ToList();
            }
        }

        public bool Excluir(Guid id)
        {
            using (var db = new LiteDB.LiteDatabase("BancoDeDados.db"))
            {
                var entradaCollection = db.GetCollection<EntradaDTO>("entradas");
                return entradaCollection.Delete(id);
            }
        }

        public EntradaDTO BuscaPorId(Guid id)
        {
            using (var db = new LiteDB.LiteDatabase("BancoDeDados.db"))
            {
                var entradaCollection = db.GetCollection<EntradaDTO>("entradas");
                return entradaCollection.FindOne(x => x.Id == id);
            }
        }

        public bool Alterar(EntradaDTO obj)
        {
            using (var db = new LiteDB.LiteDatabase("BancoDeDados.db"))
            {
                var entradaCollection = db.GetCollection<EntradaDTO>("entradas");
                var result = entradaCollection.FindOne(x => x.Id == obj.Id);

                result.Data = obj.Data;
                result.Hora = obj.Hora;
                result.Comentario = obj.Comentario;

                return entradaCollection.Update(result);
            }
        }
    }
}