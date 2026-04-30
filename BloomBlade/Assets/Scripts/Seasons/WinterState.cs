public class WinterState : ISeasonState
{
    private WeaponController weapon;
    public WinterState(WeaponController weapon) { this.weapon = weapon; }

    public void Attack() => weapon.springAttack.AttackWithStats(12, 1.8f); // daha geniþ ama zayýf
    public string GetName() => "Kýþ";
}