using System.Collections.Generic;

namespace SeaButtle2
{
    /// <summary>
    /// что является результатом выстрела? Правильно, нам нужно вернуть статус(попал, не попал итд), и ещё результат выстрела - то есть поля всех противников после выстрела, чтобы их отобразить
    /// TODO подумать, что стоит оставить в абстрактном классе, какую логику вынести и переопределить
    /// </summary>
    public class ShootCommandResult
    {
        /// <summary>
        /// Результат выстрела для каждого из противников
        /// </summary>
        public List<ShootResultField> results { get; }

        public ShootCommandResult(List<ShootResultField> results)
        {
            this.results = results;
        }

        public override string ToString()
        {
            // TODO: вывести все поля противников и результат текущего выстрела
            return base.ToString();
        }
    }

    /// <summary>
    /// Ответ после вызова команды Shoot на поле
    /// </summary>
    public class ShootResultField
    {
        public ShootResult result { get; set; }

        public string field { get; set; }
    }
}
