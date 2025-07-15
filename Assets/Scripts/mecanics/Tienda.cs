using TMPro;
using UnityEngine;

public class Tienda : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] int _bulletPrice = 150, _medicinePrice = 250;
    [SerializeField] int _medicine;
    [SerializeField] TMP_Text _bulletText, _medicineText;

    public GameObject objetoA;
    private Color _originalColor;
    [SerializeField] ParticleSystem _errorParticles;

    SoundEfects _soundEfects;
    private void Start()
    {
        _soundEfects = GetComponent<SoundEfects>();
       // _bulletText.text = "$" + _bulletPrice;
        //_medicineText.text = "$" + _medicinePrice;
        _originalColor = objetoA.GetComponent<MeshRenderer>().material.color;

    }
    public void BuyBullet()
    {
            if (_player._score >= _bulletPrice && _player._currentAmmo < _player._maxAmmo)
            {
                _soundEfects.PlaySoundFromGroup(1);
                _player.ReloadOneBullet();
                _player.LessMoney(_bulletPrice);
            }
            else
            {
                _soundEfects.PlaySoundFromGroup(3);
                TriggerErrorFeedback();
            }
    }
    public void BuyMedicine()
    {
            if(_player._score >= _medicinePrice && _player.cordura < 100)
            {
                _soundEfects.PlaySoundFromGroup(2);
                _player.cordura += _medicine;
                _player.LessMoney(_medicinePrice);
            }
            else
            {
                _soundEfects.PlaySoundFromGroup(3);
                TriggerErrorFeedback();
            }
    }
    private void TriggerErrorFeedback()
    {
        objetoA.GetComponent<MeshRenderer>().material.color = Color.red;

        if (_errorParticles != null)
        {
            _errorParticles.Play();
        }

        Invoke(nameof(ResetColor), 0.5f);
    }

    private void ResetColor()
    {
        objetoA.GetComponent<MeshRenderer>().material.color = _originalColor;
    }
}
