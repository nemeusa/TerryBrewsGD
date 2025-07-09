using TMPro;
using UnityEngine;

public class Tienda : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] int _bulletPrice = 400, _medicinePrice = 200;
    [SerializeField] int _medicine;
    [SerializeField] TMP_Text _bulletText, _medicineText;

    private void Start()
    {
        _bulletText.text = "Bullet Price: " + _bulletPrice;
        _medicineText.text = "Medicine Price: " + _medicinePrice;
    }

    private void Update()
    {
        
    }

    public void BuyBullet()
    {
        if (_player._currentAmmo < _player._maxAmmo)
            if (_player._score >= _bulletPrice)
            {
                Debug.Log("Buying bullet for price: " + _bulletPrice);
                _player.ReloadOneBullet();
                _player._score -= _bulletPrice;
            }
    }

    public void BuyMedicine()
    {
        if(_player._cordura < 100)
            if(_player._score >= _medicinePrice)
            {
                Debug.Log("Buying medicine: " + _medicine + " for price: " + _medicinePrice);
                _player._cordura += _medicine;
                _player._score -= _medicinePrice;
            }
    }
}
