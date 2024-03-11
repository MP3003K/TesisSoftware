import {students, scales} from "./data"

const initialValues = scales.map(({scales, name}, i) => ({
    id: i + 1,
    name,
    scales: scales.map(({name, indicators}, i) => ({
        id: i + 1,
        name,
        indicators: indicators.map(({name, questions}, i) => ({
            id: i + 1,
            name,
            questions,
        })),
    })),
}));

const questions =
    initialValues.flatMap((_) =>
        _.scales.flatMap((_) => _.indicators.flatMap((_) => _.questions)),
    ).map((e, i) => ({
        id: i + 1,
        name: e,
        answer: null,
    }));

const dimensions = initialValues.map(({id, name}) => ({id, name}));

const getScalesByDimension = (id: number) => {
    return initialValues.find((e) => e.id == id)?.scales ?? [];
}
