public class SpringState : ISeasonState
{
    private WeaponController weapon;
    public SpringState(WeaponController weapon) { this.weapon = weapon; }

    public void Attack() => weapon.springAttack.AttackWithStats(15, 1.2f); // 4 hit civarı
    public string GetName() => "Bahar";
}