import java.util.List;

class Knapsack {

    int[][] cachedValues;
    Item[] items;

    int maximumValue(int maximumWeight, List<Item> items) {

        this.items = items.toArray(new Item[0]);
        prepareValueCache(items.size(), maximumWeight);

        return maxValueForSubset(items.size(), maximumWeight);
    }

    void prepareValueCache(int nitems, int maximumWeight) {
        cachedValues = new int[nitems+1][maximumWeight+1];
        for (int s = 0; s <= nitems; s++) {
            for (int w = 0; w <= maximumWeight; w++) {
                cachedValues[s][w] = -1;
            }
        }
    }

    int maxValueForSubset(int nitems, int weight) {
        if ( nitems == 0 || weight <= 0)
        {
            cachedValues[nitems][weight < 0 ? 0 : weight] = 0;
            return 0;
        }

        if (cachedValues[nitems-1][weight] == -1) {
            maxValueForSubset(nitems-1, weight);
        }

        if (items[nitems-1].weight > weight) {
            // item cannot fit in the bag
            cachedValues[nitems][weight] = cachedValues[nitems-1][weight];
        } else {
            if (cachedValues[nitems-1][weight-items[nitems-1].weight] == -1) {
                // value is not cached, have to calculate it
                maxValueForSubset(nitems-1, weight-items[nitems-1].weight);
            }
            cachedValues[nitems][weight] = Math.max(
                cachedValues[nitems-1][weight],
                cachedValues[nitems-1][weight-items[nitems-1].weight] + items[nitems-1].value
            );
        }

        return cachedValues[nitems][weight];
    }
}