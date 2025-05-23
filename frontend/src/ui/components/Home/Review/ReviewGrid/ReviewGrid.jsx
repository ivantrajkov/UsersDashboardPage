import ReviewCard from "../ReviewCard";
import React from 'react';
const ReviewGrid = () => {
    return (
            // <div className="grid sm:grid-cols-2  xl:grid-cols-3 2xl:grid-cols-4 gap-1">
            <div className="grid gap-3 sm:grid-cols-1 lg:grid-cols-2 xl:grid-cols-3" data-testid="grid"> 
                <ReviewCard/>
                <ReviewCard/>
                <ReviewCard/>
                <ReviewCard/>
            </div>
    )
}
export default ReviewGrid;