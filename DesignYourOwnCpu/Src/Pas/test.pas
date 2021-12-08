{ Avaerage out a series of numbers }
{ enter one number per line and terminate with a zero }

var sum, number, current;
  begin
    sum := 0; { sum of all non-zero values read so far }
    number := 0; { number of values read }
    current := read;

    while current <> 0 do
    begin
        sum := sum + current;
        number := number + 1;
        current := read
    end;

    write(number);
    write(sum);

    if number <> 0 then 
        write(sum / number); { the average valiue }
end.
