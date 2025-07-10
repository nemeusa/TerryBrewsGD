using TMPro;
using UnityEngine;

public class Tienda : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] int _bulletPrice = 150, _medicinePrice = 250;
    [SerializeField] int _medicine;
    [SerializeField] TMP_Text _bulletText, _medicineText;

    private void Start()
    {
        _bulletText.text = "$" + _bulletPrice;
        _medicineText.text = "$" + _medicinePrice;
    }
    public void BuyBullet()
    {
        if (_player._currentAmmo < _player._maxAmmo)
            if (_player._score >= _bulletPrice)
            {
                _player.ReloadOneBullet();
                _player._score -= _bulletPrice;
            }
    }

    public void BuyMedicine()
    {
        if(_player._cordura < 100)
            if(_player._score >= _medicinePrice)
            {
                _player._cordura += _medicine;
                _player._score -= _medicinePrice;
            }
    }
}
