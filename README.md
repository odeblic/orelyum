# The Orelyum Project

Because naming matters, because it is cool, because we have fun coding.

![Orelyum](images/illustration.jpeg)

## Requirements

A list of trades previously booked is available.

| direction | symbol | quantity | price |
| --------- | ------ | -------- | ----- |
| buy       | etf-99 |     1500 | 12.78 |
| sell      | etf-99 |     1200 | 13.04 |
| buy       | stk-71 |      700 | 55.38 |
| sell      | stk-71 |      300 | 55.32 |

Calculate the position for each asset.

## Usage

How to clone and build:

```sh
git clone https://github.com/odeblic/orelyum.git
cd orelyum
dotnet run build
```

How to test and run:

```sh
dotnet run list-of-trades.txt
```

## Screenshot

Positions:

![Positions](images/screenshot.png)

## License

All the content of this repository is under MIT license.

Please check carefully the terms in the `LICENSE` file.

## Author

For any question, please contact the author: [Olivier de BLIC](mailto:odeblic@gmail.com).
