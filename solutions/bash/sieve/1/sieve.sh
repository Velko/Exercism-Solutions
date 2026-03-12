#!/usr/bin/env bash

(( $# == 1 )) || exit 1

declare -a sieve
s=""

for ((p=2; p<=$1; p++)); do
    sieve[p]=1;
done

for ((p=2; p<=$1; p++)); do
    (( sieve[p] )) || continue

    echo -n "$s$p"
    s=" "

    for ((m=p*p; m<=$1; m+=p)); do
        sieve[m]=0
    done
done

echo ""
