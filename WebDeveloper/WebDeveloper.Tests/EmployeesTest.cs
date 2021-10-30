using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebDeveloper.Core.Entities;
using WebDeveloper.Infra.Data;
using Xunit;

namespace WebDeveloper.Tests
{
    public class EmployeesTest
    {
        [Fact]
        public void InsertarUnJefeConMuchosSubordinadosDebeFuncionar()
        {
            // Preparar la prueba
            var options = new DbContextOptionsBuilder<ChinookContext>()
                .UseSqlServer("server=.;database=Chinook;trusted_connection=true;")
                .Options;
            using var context = new ChinookContext(options);

            // - Insertar Jefe
            var jefe = new Employee
            {
                FirstName = "Jefe",
                LastName = "Apellido",
                Title = "Gerente"
            };

            context.Add(jefe);
            // Guardar los cambios del jefe
            context.SaveChanges();

            // - Insertar subordinados
            var subordinados = new List<Employee>();
            var cantidadSubordinados = 200;
            for (int i = 0; i < cantidadSubordinados; i++)
            {
                subordinados.Add(new Employee
                {
                    FirstName = $"Subordinado {i + 1}",
                    LastName = "Apellido",
                    Title = "Analista",
                    ReportsTo = jefe.EmployeeId
                });
            }

            context.Employees.AddRange(subordinados);

            // Guardar los cambios
            var countSave = context.SaveChanges();
            Assert.True(countSave > 0);

            // Seleccionar al jefe
            var jefeActualizado = context.Employees.Find(jefe.EmployeeId);

            Assert.Equal(cantidadSubordinados, jefeActualizado.InverseReportsToNavigation.Count);
        }
    }
}
