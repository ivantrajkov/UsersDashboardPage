import  HomeBanner  from "../../components/Home/HomeBanner/HomeBanner"
import ReviewCard from "../../components/Home/Review/ReviewCard"
import ReviewGrid from "../../components/Home/Review/ReviewGrid/ReviewGrid"
import React from 'react';

export const Home = () => {
    return (
        <div>
            <HomeBanner/>
            <ReviewGrid/>
        </div>
    )
}