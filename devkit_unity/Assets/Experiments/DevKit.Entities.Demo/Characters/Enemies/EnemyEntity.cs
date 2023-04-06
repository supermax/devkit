using DevKit.Entities.Demo.Characters.Enemies.API;
using DevKit.Entities.Extensions;

namespace DevKit.Entities.Demo.Characters.Enemies
{
    public class EnemyEntity : CharacterEntity<IEnemyEntity>, IEnemyEntity
    {
        public override void Init()
        {
            base.Init();
            Id = this.GetId();
        }
    }
}
