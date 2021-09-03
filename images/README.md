# Built-in Methodlar

## Map() Methodu

```python
liste=["1","2","3","4"]
sonuc=list((map(int,liste)))
print(sonuc)
# liste elemanlarını integer'a çevirir. int yerine baska fonk yazılabilir.
```

## Zip() methodu

```python
liste=[1,2,3,4,5]
liste2=["bir","iki","üç","dört","beş"]
yeniliste=list(zip(liste,liste2))
print(yeniliste)
#[(1, 'bir'), (2, 'iki'), (3, 'üç'), (4, 'dört'), (5, 'beş')]
```